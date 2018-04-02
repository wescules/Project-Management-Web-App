using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class lmaoooo : System.Web.UI.Page
{
    string b;
    List<Dictionary<string, string>> initialData;

    protected void Page_Load(object sender, EventArgs e)
    {
        //make label invisble; makes value trasnfer easier
        Label1.Visible = false;

        Label1.Text = Request.QueryString["Name"].ToString();

        //load in values for Phases
        LoadPhases(Label1.Text);

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
    }
    
    //load in values for Phases
    private void LoadPhases(string id)
    {

        if (!IsPostBack)
        {   
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select PhaseName, PhaseID, CurrentPosition from Phase where Phase.ProjectId=" + id + " ORDER BY CurrentPosition ASC;";
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
            cmd.CommandText = "SELECT DISTINCT TaskID, TaskName, MAX(Tasks.CurrentPosition) as Position, E.FirstName as EmployeeName FROM Tasks, Phase, Employee as E where Phase.ProjectId = Tasks.ProjectId and Tasks.PhaseID =" + a + " and Tasks.AssignedEmployeeID = E.EmployeeID GROUP BY TaskID, TaskName, E.FirstName ORDER BY Position ASC, TaskID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            childRepeater.DataSource = dt;
            childRepeater.DataBind();
        }
    }

    [WebMethod]
    public static void ParseTaskData(List<Dictionary<string, object>> tasksData)
    {
        if (tasksData.Any())
        {
            foreach (Dictionary<string, object> dictionary in tasksData)
            {
                int phaseID = Int32.Parse((string)dictionary["phase"]);
                string taskName = (string)dictionary["task"];
                int currentPosition = (int)dictionary["position"];
                string assignedEmployee = (string)dictionary["employee"];

                UpdateDatabaseRecord(phaseID, taskName, currentPosition, assignedEmployee);
            }

        }

        // stores id_ in array
        //string[] id_language = { "To-do", "In-Progress" };
        //int sortNumber = 1;

        //// Loop over array, which contains id_ 
        //foreach (string i in id_language)
        //{
        //    // method which which fire update query order save into database
        //    updateRecord(i, sortNumber);
        //    sortNumber++;
        //}
        //Response.Redirect(Request.RawUrl);
    }

    public static void UpdateDatabaseRecord(int phaseID, string taskName, int currentPosition, string employeeName)
    {
        //string realUpdateQuery = "UPDATE Tasks 
        //                  SET Tasks.PhaseID=Phase.PhaseID, Tasks.CurrentPosition=" + currentPosition + 
        //                  " WHERE Phase.PhaseName='" + phaseName + "' AND Tasks.Title='" + taskName +"';";
        string updateQuery = "UPDATE Tasks " +
                            "SET Tasks.CurrentPosition = @position, Tasks.PhaseID = @phaseID " +
                            "FROM Tasks INNER JOIN Employee ON Tasks.AssignedEmployeeID = Employee.EmployeeID " +
                            "WHERE Tasks.TaskName = @taskName AND Employee.FirstName = @employee;";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = updateQuery;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@phaseID", phaseID);
                cmd.Parameters.AddWithValue("@taskName", taskName);
                cmd.Parameters.AddWithValue("@position", currentPosition);
                cmd.Parameters.AddWithValue("@employee", employeeName);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("Rows affected: {0}", rowsAffected);
                    conn.Close();
                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    conn.Close();
                }

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
            cmd.CommandText = "insert into Tasks(TaskName, ProjectId, PhaseID, CurrentPosition) values('ayy', 2, 2, 2)";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Response.Redirect(Request.RawUrl);
    }
}







//WORKING ON DRAG AND DROP FOR TASKS