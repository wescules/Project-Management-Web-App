using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_AdminProject :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("../Login.aspx");

        //make label invisble; makes value trasnfer easier
        Label1.Visible = false;
        Label1.Text = Request.QueryString["Name"].ToString();
        Session["department"] = Label1.Text;
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Projects.ProjectID, ProjectName, FirstName, LastName from Projects, Employee where Projects.DepartmentID = '" + Label1.Text + "' and Projects.ManagerID = Employee.EmployeeID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        AddDepartmentstoSidebar();
        AddPrivateBoards();

    }
    protected void register(string b)
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO [Projects] ([ProjectName])VALUES ('" + b + "')";

        cmd.ExecuteNonQuery();

        attach.Close();
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

    protected void button_Click(object sender, EventArgs e)
    {
        Session["department"] = Label1.Text;
        Response.Redirect("../Admin/report.aspx");
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        Session["query"] = searchInput.Text;
        Response.Redirect("../Admin/Search.aspx");
    }
    private void Insert()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        SqlCommand cmd = attach.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "insert into Projects(ProjectName, DepartmentID, isPublic, Budget, StartDate, Deadline, ManagerID) values ('" + ProjName.Text + "', " + Label1.Text + ", 1, " + Budget.Text + ", '" + StartDate.Text + "', '" + EndDate.Text + "', " + Session["emp"] + " )";
        try
        {
            //Response.Write(ProjName.Text);
            //Response.Write(Label1.Text);
            //Response.Write(Budget.Text);
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
        cmd.CommandText = "insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + phase+ " , " + Session["prgggID"] + " , '2018-03-30', 'Task 3', 1, '2018-04-03', " + Session["emp"] + ");insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + phase1 + " , " + Session["prgggID"] + " , '2018-03-30', 'Task 2', 1, '2018-04-03', " + Session["emp"] + ");insert into Tasks(PhaseID, ProjectID, StartDate, TaskName, CurrentPosition, DateCompleted, AssignedEmployeeID) values(" + phase2 + " , " + Session["prgggID"] + " , '2018-03-30', 'Task 1', 1, '2018-04-03', " + Session["emp"] + ");";


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
}
