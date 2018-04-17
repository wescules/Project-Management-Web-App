using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (Session["admin"] != null)
            {
                Response.Redirect("Admin/AdminDashboard.aspx");
            }
            else if (Session["employee"] != null)
            {
                Response.Redirect("~/Dashboard.aspx");
            }
            
        }
    }

    protected void login_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard.aspx");
    }

    //validates userinput for password. safegaurds against SQL injection
    public bool validateUserInput(String password)
    {
        var positiveIntRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]*$");
        return !positiveIntRegex.IsMatch(password) ? false : true;
    }

    protected void loginbtn_Click(object sender, EventArgs e)
    {

        bool usernameValidated = validateUserInput(txtemail.Text);
        bool passwordValidated = validateUserInput(txtpass.Text);

        //access database ONLY IF username and password are validated
        if (usernameValidated && passwordValidated)
        {
            //encrypt user/pass and create new connection
            SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            attach.Open();

            //updated to accomodate for case sensitive user/passwords
            string query = "select count(*) from Employee where FirstName COLLATE Latin1_General_CS_AS ='" + txtemail.Text + "' and LastName COLLATE Latin1_General_CS_AS ='" + txtpass.Text + "' ";
            
            SqlCommand command = new SqlCommand(query, attach);
            int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));  

            attach.Close();
            if (output == 1)
            {
                //create session
                Session["user"] = txtemail.Text;
                //direct to admin pages
                
                if (meme())
                {
                    Response.Redirect("Admin/AdminDashboard.aspx");
                    Session["admin"] = txtemail.Text;
                }
                //direct to employee pages
                else
                {
                    Response.Redirect("~/Dashboard.aspx");
                    Session["employee"] = txtemail.Text;
                }
                
            }
            else
            {
                Response.Write("Login Failed");
            }
        }
        else
        {
            Response.Write("Login Failed");
        }
    }

    protected bool meme()
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        SqlCommand cmd = new SqlCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from Employee where FirstName COLLATE Latin1_General_CS_AS ='" + txtemail.Text + "' and LastName COLLATE Latin1_General_CS_AS ='" + txtpass.Text + "' ";
        cmd.Connection = attach;

        SqlDataReader rd = cmd.ExecuteReader();
        
        while (rd.Read())
        {
            if (rd[5].ToString() == "1" || rd[5].ToString() == "True")
            {
                Session["emp"] = rd[0];
                Session["empName"] = rd[2];
                return true;
            }
            else
            {
                Session["emp"] = rd[0];
                Session["empName"] = rd[2];
            }
        }
        
        attach.Close();
        return false;

        
    }
}




