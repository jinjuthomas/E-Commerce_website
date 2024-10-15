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
    public partial class categoryedit : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Gridbind_Fn();
            }
        }
        public void Gridbind_Fn()
        {
            string sel = "select * from category";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Gridbind_Fn();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Gridbind_Fn();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            FileUpload fl = (FileUpload)GridView1.Rows[i].Cells[2].FindControl("FileUpload1");
            string p = "~/catimg/" + fl.FileName;
            fl.SaveAs(MapPath(p));
            TextBox txtdes = (TextBox)GridView1.Rows[i].Cells[3].FindControl("TextBox1");
            string up = "update category set category_image='" + p + "',category_description='" + txtdes.Text + "' where category_id=" + getid + "";
            int j = obj.Fn_Nonquery(up);
            GridView1.EditIndex = -1;
            Gridbind_Fn();
        }
    }
}