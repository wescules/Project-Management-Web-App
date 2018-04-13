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
using System.Text;


public class Task
{
    public string TaskID { get; set; }
    public string Name { get; set; }
    public string Resource { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string duration { get; set; }
    public string PercentComplete { get; set; }
    public string Dependencies { get; set; }
}

public partial class lmaoooo : System.Web.UI.Page
{
    string b, abc;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("../Login.aspx");
        //make label invisble; makes value trasnfer easier
        Label1.Visible = false;

        Label1.Text = Request.QueryString["Name"].ToString();

        //load in values for Phases
        LoadPhases(Label1.Text);
        AddPrivateBoards();
        AddDepartmentstoSidebar();
        loadTimeline();
        LoadTimelineJS();
    }

    //injects JS into aspx via string builder
    protected void LoadTimelineJS()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type=\"text/javascript\">");
        sb.Append("google.charts.load('current',{'packages':['gantt']});google.charts.setOnLoadCallback(drawChart);function drawChart(){var data=new google.visualization.DataTable();data.addColumn('string','Task ID');data.addColumn('string','Task Name');data.addColumn('string','Resource');data.addColumn('date','Start Date');data.addColumn('date','End Date');data.addColumn('number','Duration');data.addColumn('number','Percent Complete');data.addColumn('string','Dependencies');data.addRows([" +
        abc + "]);var width1=(window.innerWidth>0)?window.innerWidth:screen.width;var options={height:400,width:width1-310,gantt:{trackHeight:30}};var chart=new google.visualization.Gantt(document.getElementById('chart_div'));chart.draw(data,options)}");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "testarrayscript", sb.ToString());
    }

    //reads database and creates an array of task attributes to inject into JS
    protected void loadTimeline()
    {
        List<Task> task = new List<Task>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = new SqlCommand("select TaskName, StartDate, DateCompleted from Tasks where DateCompleted is not null and StartDate is not null", conn);
        SqlDataReader dr;
        try
        {
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                task.Add(new Task()
                {
                    TaskID = "yes",
                    Name = dr.GetString(dr.GetOrdinal("TaskName")),
                    Resource = "spring",
                    StartDate = dr.GetDateTime(dr.GetOrdinal("StartDate")),
                    EndDate = dr.GetDateTime(dr.GetOrdinal("DateCompleted")),
                    duration = "null",
                    PercentComplete = "null",
                    Dependencies = "null"
                });

            }
            dr.Close();

        }
        catch (Exception exp)
        {
            Response.Write("Null Values in database");
            throw;
        }
        finally
        {

            conn.Close();
        }

        abc = Createstring(task);
    }

    //creates a string to producted the timeline (array of arrays O(n^2))
    protected string Createstring(List<Task> task)
    {
        int j = 0, size = task.Count;
        StringBuilder str = new StringBuilder();

        foreach (var obj in task)
        {
            str.Append("[");
            int i = 0;
            foreach (var prop in obj.GetType().GetProperties())
            {
                var a = prop.GetValue(obj, null).ToString();
                if (i < 7)
                {
                    if (char.IsDigit(a[0]))
                    {
                        DateTime oDate = DateTime.Parse(a);
                        string q = oDate.Year + ", " + oDate.Month + ", " + oDate.Day;
                        str.Append("new Date(" + q + "), ");
                    }
                    else
                    {
                        if (i == 6)
                        {
                            str.Append(100 + ", ");
                        }
                        else
                        {
                            if (prop.GetValue(obj, null).Equals("null"))
                            {
                                str.Append("null" + ", ");
                            }
                            else
                            {
                                if (i == 0 || i == 2)
                                {
                                    str.Append("\'" + prop.GetValue(obj, null) + j + "\'" + ", ");
                                }
                                else
                                {
                                    str.Append("\'" + prop.GetValue(obj, null) + "\'" + ", ");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (prop.GetValue(obj, null).Equals("null"))
                    {
                        str.Append("null");
                    }

                }
                i++;
            }

            if (j == size - 1)
            {
                str.Append("]");
            }
            else
            {
                str.Append("], ");
            }
            j++;
        }
        return str.ToString();
    }
    protected void AddDepartmentstoSidebar()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select DepartmentID, DepartmentName from Department";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
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
    protected void AddPrivateBoards()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select ProjectID, ProjectName from Projects where ManagerID=" + Session["emp"] + " and isPublic=0 ";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater3.DataSource = dt;
        Repeater3.DataBind();
    }
}
//WORKING ON DRAG AND DROP FOR TASKS
