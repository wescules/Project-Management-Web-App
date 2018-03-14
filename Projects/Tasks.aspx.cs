using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class lmaoooo : System.Web.UI.Page
{
    string b;
    protected void Page_Load(object sender, EventArgs e)
    {
        //make label invisble; makes value trasnfer easier
        Label1.Visible = false;

        Label1.Text = Request.QueryString["Name"].ToString();

        //load in values for Phases
        LoadProjects(Label1.Text);

    }
    
    //load in values for Phases
    private void LoadProjects(string id)
    {

        if (!IsPostBack)
        {   
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select GroupName, GroupId from Group1 where Group1.ProjectId=" + id + ";";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            this.ParentRepeater.DataSource = dt;
            this.ParentRepeater.DataBind();
        }
    }

    //load in values foreach task in phase
    protected void ItemBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = args.Item.FindControl("lblName") as Label;      //Assignment.groupID
            var a = lbl.Text;
            




            Repeater childRepeater = (Repeater)args.Item.FindControl("ChildRepeater");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select AssignmentNote, AssignmentEnd, Position from Assignment, Group1 where Group1.ProjectId = Assignment.ProjectId and Assignment.GroupID =" + a + " group by AssignmentNote, AssignmentEnd, Position order by MAX(Assignment.Position) asc;";
            cmd.CommandText = "SELECT DISTINCT AssignmentId, AssignmentNote, AssignmentEnd, MAX(Assignment.Position) as Position FROM Assignment, Group1 where Group1.ProjectId = Assignment.ProjectId and Assignment.GroupID =" + a + " GROUP BY AssignmentId, AssignmentNote, AssignmentEnd ORDER BY Position DESC, AssignmentId";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            childRepeater.DataSource = dt;
            childRepeater.DataBind();
        }
    }

    protected void btnUpdateSortOrder_Click(object sender, EventArgs e)
    {
        // stores id_ in array
        string[] id_language = { "To-do", "In-Progress" };
        int sortNumber = 1;

        // Loop over array, which contains id_ 
        foreach (string i in id_language)
        {
            // method which which fire update query order save into database
            updateRecord(i, sortNumber);
            sortNumber++;
        }
        Response.Redirect(Request.RawUrl);
    }

    public void updateRecord(string languageId, int sortOrder)
    {
        string updateQuery = "UPDATE Assignment SET Position =" + sortOrder + " WHERE GroupName=" + languageId + ";";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = updateQuery;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id_", languageId);
                cmd.Parameters.AddWithValue("@sort_order", sortOrder);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    protected void ChildRepeater_ItemDataBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = args.Item.FindControl("Label2") as Label;      //Assignment.Position
            b = lbl.Text.ToString();
            Response.Write(b);

        }
    }

    protected void RepeaterDetailsRow_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "addnew")
        {
            Repeater childRepeater = (Repeater)e.Item.FindControl("ChildRepeater");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Assignment(AssignmentNote, ProjectId, GroupID, position) values('ayy', 1, 1, 2)";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Response.Redirect(Request.RawUrl);
    }
}







//WORKING ON DRAG AND DROP FOR TASKS