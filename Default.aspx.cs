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
       if(Session["user"] != null)
        {
            //Response.Redirect("~/Welcome.aspx");
        }
    }

    protected void loginbtn_Click(object sender, EventArgs e)
    {
        //encrypt user/pass and create new connection
        SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        attach.Open();
        string query = "select count(*) from People where FirstName='" + txtemail.Text + "' and LastName='" + txtpass.Text + "' ";
        Response.Write(query);
        SqlCommand command = new SqlCommand(query, attach);
        int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));

        attach.Close();
        if (output == 1)
        {
            //create session
            Session["user"] = txtemail.Text;
            //Response.Write("Login Success");
            Response.Redirect("~/Welcome.aspx");
        }
        else
        {
            Response.Write("Login Failed");
        }
    }
}









//protected void loginbtn_Click(object sender, EventArgs e)
//{
//    //encrypt user/pass and create new connection
//    SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
//    attach.Open();
//    string query = "select count(*) from People where FirstName='" + txtuser.Text + "' and LastName='" + txtpass.Text + "' ";
//    Response.Write(query);
//    SqlCommand command = new SqlCommand(query, attach);
//    int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));

//    attach.Close();
//    if (output == 1)
//    {
//        //create session
//        Session["user"] = txtuser.Text;
//        //Response.Write("Login Success");
//        Response.Redirect("~/Welcome.aspx");
//    }
//    else
//    {
//        Response.Write("Login Failed");
//    }
//}

//protected void loginbtn_Click(object sender, EventArgs e)
//{
//    //encrypt user/pass and create new connection
//    SqlConnection attach = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
//    attach.Open();
//    string checkUser = "select count(*) from People where FirstName='" + txtuser.Text + "' ";

//    Response.Write(checkUser);
//    SqlCommand command = new SqlCommand(checkUser, attach);
//    int output = Convert.ToInt32(command.ExecuteScalar().ToString().Replace(" ", ""));

//    attach.Close();

//    //check matching password
//    if (output == 1)
//    {
//        attach.Open();
//        string checkPass = "select count(*) from People where LastName='" + txtpass.Text + "' ";

//        //Response.Write(checkPass);
//        SqlCommand com = new SqlCommand(checkPass, attach);
//        string password = com.ExecuteScalar().ToString().Replace(" ", "");

//        if (password == txtpass.Text)
//        {
//            Session["New"] = txtuser.Text;
//            Response.Write("Password is correct");
//        }
//        else
//        {
//            Response.Write("Password is NOT correct");
//        }
//        attach.Close();

//        //Response.Write("Login Success");
//        Response.Redirect("~/Welcome.aspx");
//    }
//    else
//    {
//        Response.Write("User Name NOT correct. Login Failed");
//    }
//}