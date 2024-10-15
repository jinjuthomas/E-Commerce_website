using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace project41
{
    public partial class paymentdetailsadmin : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            Fn_Gridbind();
        }
        public void Fn_Gridbind()
        {
            string sel = "select * from Bill_Tab where bill_status='Paid'";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Fn_Gridbind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView1.EditIndex = -1;
            Fn_Gridbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtsta = (TextBox)GridView1.Rows[i].Cells[3].FindControl("TextBox1");
            string up = "Update bill_tab set bill_status='Delivered' where Bill_id="+getid+"";
            obj.Fn_Nonquery(up);
            GridView1.EditIndex = -1;
            Fn_Gridbind();
        }
    }
}