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
using System.Net.Mail;


public class Task1
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

public partial class Admin_AdminTasks : System.Web.UI.Page
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
        SetNameAndDescription();
    }

    private void SetNameAndDescription()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select ProjectName, Description from Projects where Projects.ProjectID = " + Label1.Text;
        cmd.Connection = con;

        SqlDataReader rd = cmd.ExecuteReader();

        do
        {
            rd.Read();
            projTitle.Text = (string)rd[0];
            if (!(rd[1] is DBNull))
                projDescription.Text = (string)rd[1];
        }
        while (rd.Read());

        con.Close();

    }

    protected void Search_Click(object sender, EventArgs e)
    {
        Session["query"] = searchInput.Text;
        Response.Redirect("../Admin/Search.aspx");
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
        List<Task1> task = new List<Task1>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = new SqlCommand("select TaskName, StartDate, DateCompleted from Tasks where ProjectID=" + Label1.Text, conn);
        SqlDataReader dr;
        try
        {
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                task.Add(new Task1()
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
    protected string Createstring(List<Task1> task)
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
        con.Close();
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
            con.Close();
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
            //cmd.CommandText = "SELECT DISTINCT TaskID, TaskName, MAX(Tasks.CurrentPosition) as Position, E.FirstName as EmployeeName FROM Tasks, Phase, Employee as E where Phase.ProjectId = Tasks.ProjectId and Tasks.PhaseID =" + a + " and Tasks.AssignedEmployeeID = E.EmployeeID GROUP BY TaskID, TaskName, E.FirstName ORDER BY Position ASC, TaskID";
            cmd.CommandText = "SELECT DISTINCT TaskID, TaskName, MAX(Tasks.CurrentPosition) as Position, E.FirstName as EmployeeName FROM Tasks, Phase, Employee as E, Projects where Tasks.ProjectId = " + Label1.Text + " and Tasks.PhaseID =" + a + " and Tasks.AssignedEmployeeID = E.EmployeeID GROUP BY TaskID, TaskName, E.FirstName ORDER BY Position ASC, TaskID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            childRepeater.DataSource = dt;
            childRepeater.DataBind();
            con.Close();
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
    protected void meme()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from Phase where ProjectID=" + Label1.Text +" and CurrentPosition=1";
        cmd.Connection = attach;

        SqlDataReader rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            
                Session["phaseID"] = rd[0];
             
        }

        attach.Close();
        


    }

    private void Insert()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition,  DateCompleted, AssignedEmployeeID) values (" + Session["phaseID"] + "," + Label1.Text + " , '" + StartDate.Text + "', '" + TaskName.Text + "', 1, '" + EndDate.Text + "', " + Session["emp"] + ")";
        try
        {
            //Response.Write(Session["phaseID"]);
            //Response.Write(Label1.Text);
            //Response.Write(StartDate.Text);
            //Response.Write(TaskName.Text);
            //Response.Write(EndDate.Text);
            //Response.Write(Session["emp"]);

            attach.Open();
            cmd.ExecuteNonQuery();
            Response.Write("Task Saved");
        }
        catch
        {
            Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }
        StartDate.Text = "";
        EndDate.Text = "";
        

        attach.Close();
    }
    protected void click1(object sender, EventArgs e)
    {
        StartDate.Text += ":00";
        EndDate.Text += ":00";
        meme();//gets phaseID for query
        Response.Write(StartDate.Text);

        Response.Write(EndDate.Text);
        Insert();
        Response.Redirect(Request.RawUrl);

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
            //Response.Write(b);

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



    private void Insert1()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "insert into Projects(ProjectName, isPublic, StartDate, Deadline, ManagerID) values ('" + ProjName.Text + "', 0,'" + StartDate.Text + "', '" + EndDate.Text + "'," + Session["emp"] + " );";
        try
        {
            //Response.Write(ProjName.Text);
            //Response.Write(StartDate.Text);
            //Response.Write(EndDate.Text);
            //Response.Write(Session["emp"]);


            attach.Open();
            cmd.ExecuteNonQuery();
            Response.Write("Project Saved");
        }
        catch
        {
            Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }
        StartDate.Text = "";
        EndDate.Text = "";


        attach.Close();
    }

    protected void getProjectID()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 * FROM Projects ORDER BY ProjectID DESC";
        cmd.Connection = attach;

        SqlDataReader rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            Session["prgID"] = rd[0];
        }

        attach.Close();
    }
    protected void getPhaseID()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT TOP 1 * FROM Phase ORDER BY PhaseID DESC";
        cmd.Connection = attach;

        SqlDataReader rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            Session["phsID"] = rd[0];
            Session["prgggID"] = rd[1];
        }

        attach.Close();
    }

    protected void addTemplatedPhases_tasks()
    {
        getProjectID();

        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "insert into Phase(ProjectID, PhaseName, CurrentPosition) values(" + Session["prgID"] + ", 'In-Progress', 1); insert into Phase(ProjectID, PhaseName, CurrentPosition) values(" + Session["prgID"] + ", 'To-Do', 2);insert into Phase(ProjectID, PhaseName, CurrentPosition) values(" + Session["prgID"] + ", 'Completed', 3);";

        attach.Open();
        cmd.ExecuteNonQuery();
        Response.Write("Project Saved");
        getPhaseID();
        insertTasks();

        attach.Close();
    }
    protected void insertTasks()
    {
        getProjectID();
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        int phase = Convert.ToInt32(Session["phsID"]);
        int phase1 = phase - 1;
        int phase2 = phase1 - 1;
        cmd.CommandText = "insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + phase + " , " + Session["prgggID"] + " , '2018-03-30', 'Task 3', 1, '2018-04-03', " + Session["emp"] + ");insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + phase1 + " , " + Session["prgggID"] + " , '2018-03-30', 'Task 2', 1, '2018-04-03', " + Session["emp"] + ");insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + phase2 + " , " + Session["prgggID"] + " , '2018-03-30', 'Task 1', 1, '2018-04-03', " + Session["emp"] + ");";


        attach.Open();
        cmd.ExecuteNonQuery();



        attach.Close();
    }

    protected void button2_Click(object sender, EventArgs e)
    {
        StartDate.Text += ":00";
        EndDate.Text += ":00";

        Insert1();
        addTemplatedPhases_tasks();
        Response.Redirect(Request.RawUrl);
    }

    protected void AddNewDepartmentButton(object sender, EventArgs e)
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "insert into Department(ManagerID, DepartmentName) values (" + Session["emp"] + ", '" + ProjNameDept.Text + "')";
        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
            Response.Write("Department Saved");
        }
        catch
        {
            Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }
        attach.Close();
        Response.Redirect(Request.RawUrl);
    }
    protected void findMAxPhasePostion()
    {
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select max(CurrentPosition) from Phase where ProjectID=" + Label1.Text;
        attach.Open();
        cmd.ExecuteNonQuery();
        SqlDataReader rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            Session["maxPhase"] = rd[0];
        }

        attach.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        getProjectID();
        findMAxPhasePostion();
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        int phase = Convert.ToInt32(Session["phsID"]);
        int max;
        try { max = Convert.ToInt32(Session["maxPhase"]) + 1 ; } catch
        {
             max = 1;
        }
        
        //Response.Write("   ");
        //Response.Write(Label1.Text);
        //Response.Write("   ");
        //Response.Write(phaseee.Text);
        //Response.Write("   ");
        //Response.Write(max);
        cmd.CommandText = "insert into Phase(ProjectID, PhaseName, CurrentPosition) values(" + Label1.Text + ", '" + phaseee.Text + "', " + max + ")";

        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            Response.Write("   ");
            Response.Write(Session["phsID"]);
            Response.Write("   ");
            Response.Write(Session["prgggID"]);
            Response.Write("   ");
            Response.Write(Session["emp"]);
        }

        attach.Close();
        
        getPhaseID();
        insertTask4NewPhase();
        Response.Redirect(Request.RawUrl);
    }

    protected void insertTask4NewPhase()
    {
        getProjectID();
        getPhaseID();
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        int phase = Convert.ToInt32(Session["phsID"]);
        int max;
        try {
            max = Convert.ToInt32(Session["maxPhase"]) + 1;
        } catch
        {
            max = 1;
        }
        cmd.CommandText = "insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + Session["phsID"] + " , " + Session["prgggID"] + " , '2018-03-30', 'New Task', 1, '2018-04-03', " + Session["emp"] + ")";

        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            Response.Write("   ");
            Response.Write(Session["phsID"]);
            Response.Write("   ");
            Response.Write(Session["prgggID"]);
            Response.Write("   ");
            Response.Write(Session["emp"]);
        }
        attach.Close();
       // Response.Redirect(Request.RawUrl);
    }



    protected void sendMail(object sender, EventArgs e)
    {
        if (CheckIfEmailExists())
        {
            SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            SqlCommand cmd = attach.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Works_On(ProjectID, EmployeeID, DepartmentID) values("+Label1.Text+","+ Session["empID"]+","+Session["department"] +")";
            try
            {
                attach.Open();
                cmd.ExecuteNonQuery();
                Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Added User...');if(alert){}</script>");
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add("" + Session["empEmail"]);
                mail.From = new MailAddress("projectmanagement.dash1@gmail.com", "DASH", System.Text.Encoding.UTF8);
                mail.Subject = "INVITE from " + Session["empName"] + ": Dash Project Management";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = Session["empName"] + " has added you to a Project";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("projectmanagement.dash1@gmail.com", "Dankmeme1234!");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                try
                {
                    client.Send(mail);
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Sent...');if(alert){}</script>");
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    string errorMessage = string.Empty;
                    while (ex2 != null)
                    {
                        errorMessage += ex2.ToString();
                        ex2 = ex2.InnerException;
                    }
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){}</script>");
                }

            }
            catch
            {
                Response.Write("Error adding user to Project");
                attach.Close();
            }

            attach.Close();
        }
        else
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email.Text);
            mail.From = new MailAddress("projectmanagement.dash1@gmail.com", "DASH", System.Text.Encoding.UTF8);
            mail.Subject = "INVITE from " + Session["empName"] +": Dash Project Management";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = Session["empName"] + " would Like you to join Dash: http://localhost:55142/Register.aspx";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("projectmanagement.dash1@gmail.com", "Dankmeme1234!");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Sent...');if(alert){}</script>");
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){}</script>");
            }
        }
    }
    protected bool CheckIfEmailExists()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from Employee where Email COLLATE Latin1_General_CS_AS ='" + email.Text+"'";
        cmd.Connection = attach;

        SqlDataReader rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            if (rd[8].ToString().Length > 1)
            {
                Session["empID"] = rd[0];
                return true;
            }
        }

        attach.Close();
        return false;
    }
}
//WORKING ON DRAG AND DROP FOR TASKS
