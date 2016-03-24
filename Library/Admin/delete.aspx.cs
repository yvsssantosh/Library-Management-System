﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Library.Admin
{
    public partial class a_delete : System.Web.UI.Page
    {
        String strtemp = null;
        private void GetCustomersPageWise(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SP_GET_BOOKS_PAGEWISE", con);
            IDataReader idr = null;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SEARCHTAG", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@PAGEINDEX", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PAGESIZE", SqlDbType.Int));
                cmd.Parameters["@SEARCHTAG"].Value = tb_search.Text.Trim();
                cmd.Parameters["@PAGEINDEX"].Value = pageIndex;
                cmd.Parameters["@PAGESIZE"].Value = int.Parse(tb_records_per_page.Text.Trim());
                cmd.Parameters.Add("@RECORDCOUNT", SqlDbType.Int, 4);
                cmd.Parameters["@RECORDCOUNT"].Direction = ParameterDirection.Output;
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
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RECORDCOUNT"].Value);
                    this.PopulatePager(recordCount, pageIndex);
                }
                catch (Exception exe)
                {
                   System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + exe.Message + "')</script>");
                }
            }
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / decimal.Parse(tb_records_per_page.Text));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String str = Session["admin"].ToString();
                if (int.Parse(str) != 1)
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            catch (Exception ex)
            {
               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('Please Login First!!')</script>");
                Response.Redirect("../Login.aspx");
            }
            
        }
        protected void tb_records_per_page_TextChanged(object sender, System.EventArgs e)
        {
            this.GetCustomersPageWise(1);
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.GetCustomersPageWise(pageIndex);
        }
        protected void Button1_Click1(object sender, System.EventArgs e)
        {

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string constring = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            try
            {
                
                SqlCommand cmd = new SqlCommand("SP_DELETE_BOOK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ACCESSION_NO", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@REASON_FOR_DELETE", SqlDbType.VarChar));
                int index = e.RowIndex;
                cmd.Parameters["@ACCESSION_NO"].Value = GridView1.Rows[index].Cells[1].Text;
                cmd.Parameters["@REASON_FOR_DELETE"].Value = tb_reason.Text.Trim();
                con.Open();
                cmd.ExecuteNonQuery();
               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('Book Deleted Successfully !')</script>");
            }
            catch (Exception ex)
            {
               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
                lb_reason.Visible = false;
                tb_reason.Visible = false;
                tb_reason.Text = "";
                tb_search.Text = "";
                GridView1.EditIndex = -1;
                this.GetCustomersPageWise(1);
               
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            strtemp = GridView1.SelectedRow.Cells[2].Text.ToString();
        }
        protected void Button1_Click(object sender, System.EventArgs e)
        {
            lb_reason.Visible = true;
            tb_reason.Visible = true;
            this.GetCustomersPageWise(1);
        }
        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    (e.Row.Cells[0].Controls[delet]).OnClientClick = "return confirm('Are you sure you want to delete?');"; // add any JS you want here
            //}
        }
    }
}