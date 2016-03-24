using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library
{
    public partial class s_profile : System.Web.UI.Page
    {
        string student_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            student_id = Session["stu_id"].ToString();
            try
            {
                String str = Session["stu"].ToString();
                if (int.Parse(str) != 3)
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Login.aspx");
               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('Please Login First!!')</script>");
            }
            string constring = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SP_GET_STUDENT_PROFILE_DETAILS", con);
            IDataReader idr = null;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@STUDENT_ID", SqlDbType.VarChar));
                cmd.Parameters["@STUDENT_ID"].Value = student_id.Trim();
                con.Open();
                idr = cmd.ExecuteReader();
                GridView1.DataSource = idr;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                try
                {
                    idr.Close();
                    con.Close();
                }
                catch (Exception exe)
                {
                   System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + exe.Message + "')</script>");
                }

            }
            IDataReader idr1 = null;
            cmd = new SqlCommand("SP_GET_STUDENT_PROFILE_LOAN", con);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@STUDENT_ID", SqlDbType.VarChar));
                cmd.Parameters["@STUDENT_ID"].Value = student_id.Trim();
                con.Open();
                idr1 = cmd.ExecuteReader();
                GridView2.DataSource = idr1;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                try
                {
                    idr.Close();
                    con.Close();
                }
                catch (Exception exe)
                {
                   System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + exe.Message + "')</script>");
                }

            }
        }
    }
}