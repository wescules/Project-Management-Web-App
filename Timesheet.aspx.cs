using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Timesheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        if (!IsPostBack)
        {
            
        }
        
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
        con.Close();
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