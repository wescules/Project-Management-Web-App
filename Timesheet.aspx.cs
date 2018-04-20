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

public partial class Timesheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");

        DateTime startDate = GetStartDate();

        week.Text = startDate.ToString("MM/dd/yyyy") + " - " + startDate.AddDays(7).ToString("MM/dd/yyyy");

        AddDepartmentstoSidebar();
        AddPrivateBoards();

        if (!IsPostBack)
        {
            if (TimesheetExists())
                AddDataToTimesheet();

            else
                FillBlankTimesheet();
        }


    }
    protected void Search_Click(object sender, EventArgs e)
    {
        Session["query"] = searchInput.Text;
        Response.Redirect("EmpSearch.aspx");
    }

    protected void FillBlankTimesheet()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select Projects.ProjectID, Projects.ProjectName from Projects, Works_On where Projects.ProjectID = Works_On.ProjectID and Works_On.EmployeeID = " + Session["emp"];
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        dt = FillZeros(dt);
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        con.Close();
    }

    private DataTable FillZeros(DataTable dt)
    {
        dt.Columns.Add("SunHours", typeof(string));
        dt.Columns.Add("MonHours", typeof(string));
        dt.Columns.Add("TuesHours", typeof(string));
        dt.Columns.Add("WedsHours", typeof(string));
        dt.Columns.Add("ThursHours", typeof(string));
        dt.Columns.Add("FriHours", typeof(string));
        dt.Columns.Add("SatHours", typeof(string));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["SunHours"] = "0";
            dt.Rows[i]["MonHours"] = "0";
            dt.Rows[i]["TuesHours"] = "0";
            dt.Rows[i]["WedsHours"] = "0";
            dt.Rows[i]["ThursHours"] = "0";
            dt.Rows[i]["FriHours"] = "0";
            dt.Rows[i]["SatHours"] = "0";
        }

        return dt;
    }

    protected bool TimesheetExists()
    {
        DateTime startDate = GetStartDate();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select TimesheetID from Timesheet where EmployeeID = " + Session["emp"] + " AND StartDate = '" + startDate.ToString("MM/dd/yyyy") + "'";
        cmd.Connection = con;

        SqlDataReader rd = cmd.ExecuteReader();

        bool exists = rd.Read();
        con.Close();

        return exists;
    }

    protected void AddDataToTimesheet()
    {
        DateTime startDate = DateTime.Today;
        while (startDate.DayOfWeek != DayOfWeek.Sunday)
            startDate = startDate.AddDays(-1);

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select Projects.ProjectID, Projects.ProjectName, Timesheet.TimesheetID, Hours_Worked.SunHours, Hours_Worked.MonHours, Hours_Worked.TuesHours, Hours_Worked.WedsHours, Hours_Worked.ThursHours, Hours_Worked.FriHours, Hours_Worked.SatHours " +
            "from Projects inner join Hours_Worked on Projects.ProjectID = Hours_Worked.ProjectID inner join Timesheet on Hours_Worked.TimesheetID = Timesheet.TimeSheetID " +
            "where Timesheet.StartDate = '" + startDate.ToString("MM/dd/yyyy") + "' and Timesheet.EmployeeID = " + Session["emp"];
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        con.Close();
    }

    protected void AddDepartmentstoSidebar()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct Department.DepartmentID, Department.DepartmentName from Department, Projects, Works_On where Department.DepartmentID = Projects.DepartmentID and Projects.ProjectID = Works_On.ProjectID and Works_On.EmployeeID = " + Session["emp"];
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
        con.Close();
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
        con.Close();
    }

    private void InsertHours(List<Dictionary<string, string>> rowsData)
    {
        int timesheetID = GetNewTimesheetID();

        foreach (Dictionary<string, string> row in rowsData)
        {
            InsertRecord(timesheetID, row);
        }

        Response.Redirect(Request.RawUrl);
        Response.Write("Timesheet saved");

    }

    private void InsertRecord(int timesheetID, Dictionary<string, string> rowData)
    {
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO Hours_Worked VALUES (" + timesheetID + ", " + rowData["project"] + ", " + rowData["sun"] +
                                                            ", " + rowData["mon"] + ", " + rowData["tue"] + ", " + rowData["wed"] +
                                                            ", " + rowData["thu"] + ", " + rowData["fri"] + ", " + rowData["sat"] + ", " + rowData["total"] + ")";
        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
            //Response.Write("TimeSheet Saved");
        }
        catch
        {
            //Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }

        attach.Close();

    }

    private int GetNewTimesheetID()
    {
        // Gets the timesheetID of the timesheet that was just entered, should be unique
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select TimesheetID from Timesheet where EmployeeID = " + Session["emp"] + " AND StartDate = '" + GetStartDate().ToString("MM/dd/yyyy") + "'";
        cmd.Connection = con;

        SqlDataReader rd = cmd.ExecuteReader();

        int timesheetID;

        do
        {
            rd.Read();
            timesheetID = (int)rd[0];
        }
        while (rd.Read());

        con.Close();

        return timesheetID;
    }

    private DateTime GetStartDate()
    {
        DateTime startDate = DateTime.Today;
        while (startDate.DayOfWeek != DayOfWeek.Sunday)
            startDate = startDate.AddDays(-1);

        return startDate;
    }

    private void InsertTimesheet()
    {
        DateTime startDate = GetStartDate();

        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO Timesheet (EmployeeID, StartDate) VALUES (" + Session["emp"] + ", '" + startDate + "');";
        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
            //Response.Write("Timesheet Saved");
        }
        catch
        {
            //Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }
        attach.Close();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        List<Dictionary<string, string>> rows = GetUserInputData();

        if (!TimesheetExists())
        {
            InsertTimesheet();

            foreach (Dictionary<string, string> row in rows)
            {
                foreach (KeyValuePair<string, string> keyValue in row)
                {
                    System.Diagnostics.Debug.WriteLine("{" + keyValue.Key + " : " + keyValue.Value + "}");
                }
                System.Diagnostics.Debug.WriteLine("");
            }

            InsertHours(rows);
        }

        else
        {
            foreach (Dictionary<string, string> row in rows)
            {
                foreach (KeyValuePair<string, string> keyValue in row)
                {
                    System.Diagnostics.Debug.WriteLine("{" + keyValue.Key + " : " + keyValue.Value + "}");
                }
                System.Diagnostics.Debug.WriteLine("");
            }

            UpdateHours(rows);
        }

    }

    private void UpdateHours(List<Dictionary<string, string>> rowsData)
    {
        int timesheetID = GetNewTimesheetID();

        foreach (Dictionary<string, string> row in rowsData)
        {
            UpdateRecord(timesheetID, row);
        }

        Response.Redirect(Request.RawUrl);
        Response.Write("Timesheet saved");
    }

    private void UpdateRecord(int timesheetID, Dictionary<string, string> rowData)
    {
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE Hours_Worked SET SunHours =  " + rowData["sun"] + ", MonHours = " + rowData["mon"] + ", TuesHours = " + rowData["tue"] + ", WedsHours = " + rowData["wed"] +
                            ", ThursHours = " + rowData["thu"] + ", FriHours = " + rowData["fri"] + ", SatHours = " + rowData["sat"] + ", totalHours = " + rowData["total"] +
                            " WHERE TimesheetID = " + GetNewTimesheetID() + " AND ProjectID = " + rowData["project"];
        try
        {
            attach.Open();
            cmd.ExecuteNonQuery();
            //Response.Write("TimeSheet Saved");
        }
        catch
        {
            //Response.Write("Error when saving on database. Please input values");
            attach.Close();
        }

        attach.Close();
    }

    private List<Dictionary<string, string>> GetUserInputData()
    {
        List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

        foreach (RepeaterItem item in Repeater1.Items)
        {
            Dictionary<string, string> row = new Dictionary<string, string>();

            row["project"] = ((Label)item.FindControl("projID")).Text;
            row["sun"] = ((TextBox)item.FindControl("sunHours")).Text;
            row["mon"] = ((TextBox)item.FindControl("monHours")).Text;
            row["tue"] = ((TextBox)item.FindControl("tueHours")).Text;
            row["wed"] = ((TextBox)item.FindControl("wedHours")).Text;
            row["thu"] = ((TextBox)item.FindControl("thuHours")).Text;
            row["fri"] = ((TextBox)item.FindControl("friHours")).Text;
            row["sat"] = ((TextBox)item.FindControl("satHours")).Text;
            double sum = Double.Parse(row["sun"]) + Double.Parse(row["mon"]) + Double.Parse(row["tue"]) + Double.Parse(row["wed"])
                        + Double.Parse(row["thu"]) + Double.Parse(row["fri"]) + Double.Parse(row["sat"]);
            row["total"] = sum.ToString();

            rows.Add(row);
        }

        return rows;
    }
    private void Insert()
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

        Insert();
        addTemplatedPhases_tasks();
        Response.Redirect(Request.RawUrl);
    }
}