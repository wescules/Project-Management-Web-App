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
            Label lbl = args.Item.FindControl("lblName") as Label;
            var a = lbl.Text;


            Repeater childRepeater = (Repeater)args.Item.FindControl("ChildRepeater");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct AssignmentNote, AssignmentEnd from Assignment, Group1 where Group1.ProjectId=Assignment.ProjectId and Assignment.GroupID=" + a + ";";
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
        string[] id_language = Request.Form.GetValues("languageId");
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
        string updateQuery = "UPDATE Group1 SET GroupId =" + languageId + " WHERE position=" + sortOrder + ";";
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


}







//WORKING ON DRAG AND DROP FOR TASKS