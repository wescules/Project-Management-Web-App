using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("../Login.aspx");
       
        
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) OVER () as cnt, c.* FROM Tasks c where freetext(*, '" + Session["query"] + "')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                
                //specifically update the number of results returned
                Object o = dt.Rows[0]["cnt"];
                Label1.Text = Convert.ToString(o);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            catch (Exception)
            {
                Response.Write("Search Bar is Empty Please enter Value");
            }
        
        AddPrivateBoards();
        AddDepartmentstoSidebar();
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
    protected void logoutbtn_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        Response.Redirect("~/Login.aspx");
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

    protected void button2_Click(object sender, EventArgs e)
    {
        StartDate.Text += ":00";
        EndDate.Text += ":00";

        Insert();
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