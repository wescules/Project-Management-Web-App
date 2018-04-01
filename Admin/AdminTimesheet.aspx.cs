using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
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

public partial class Admin_AdminTimesheet : System.Web.UI.Page
{

    string abc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("../Login.aspx");
        if (!IsPostBack)
        {
            LoadProjects();
        }
        AddDepartmentstoSidebar();
        loadTimeline();
        LoadTimelineJS();
    }

    //injects JS into aspx via string builder
    protected void LoadTimelineJS()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("google.charts.load('current',{'packages':['gantt']});google.charts.setOnLoadCallback(drawChart);function drawChart(){var data=new google.visualization.DataTable();data.addColumn('string','Task ID');data.addColumn('string','Task Name');data.addColumn('string','Resource');data.addColumn('date','Start Date');data.addColumn('date','End Date');data.addColumn('number','Duration');data.addColumn('number','Percent Complete');data.addColumn('string','Dependencies');data.addRows([" +
        abc + "]);var options={height:400,width:1000,gantt:{trackHeight:30}};var chart=new google.visualization.Gantt(document.getElementById('chart_div'));chart.draw(data,options)}");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "testarrayscript", sb.ToString());
    }

    //reads database and creates an array of task attributes to inject into JS
    protected void loadTimeline()
    {
        List<Task> task = new List<Task>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = new SqlCommand("select AssignmentNote, AssignmentStart, AssignmentEnd from Assignment where AssignmentEnd is not null and AssignmentStart is not null", conn);
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
                    Name = dr.GetString(dr.GetOrdinal("AssignmentNote")),
                    Resource = "spring",
                    StartDate = dr.GetDateTime(dr.GetOrdinal("AssignmentStart")),
                    EndDate = dr.GetDateTime(dr.GetOrdinal("AssignmentEnd")),
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
        cmd.CommandText = "select ProjectId, ProjectName from Project";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }

    private void LoadProjects()
    {

        DataTable subjects = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString()))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ProjectName FROM Project", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(subjects);

                ddlSubject.DataSource = subjects;
                ddlSubject.DataTextField = "ProjectName";
                ddlSubject.DataBind();
            }
            catch (Exception ex)
            {
                // Handle the error
            }

        }
    }

    private void Insert(string a, string b)
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO[Assignment]([AssignmentStart] ,[AssignmentEnd])VALUES ('" + a + "','" + b + "')";
        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
            Response.Write("TimeSheet Saved");
        }
        catch
        {
            Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }
        starttime.Text = "";
        endtime.Text = "";
        totalhours.Text = "";

        attach.Close();
    }
    protected void Submitbtn_Click(object sender, EventArgs e)
    {
        starttime.Text += ":00";
        endtime.Text += ":00";
        Response.Write(starttime.Text);

        Response.Write(endtime.Text);
        Insert(starttime.Text, endtime.Text);



    }
}