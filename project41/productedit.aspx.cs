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
    public partial class productedit : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sel = "select category_id,category_name from category";
                    DataSet ds = obj.Fn_Dataset(sel);
                DropDownList1.DataSource = ds;
                DropDownList1.DataValueField = "Category_Id";
                DropDownList1.DataTextField = "Category_Name";
                DropDownList1.DataBind();
            }
        }
        public void gridbind_fn()
        {
            string sel = "select * from product_ta where Category_id="+DropDownList1.SelectedItem.Value+"";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            gridbind_fn();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gridbind_fn();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtpri = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextBox1");
            TextBox txtsto = (TextBox)GridView1.Rows[i].Cells[3].FindControl("TextBox2");
            FileUpload fl = (FileUpload)GridView1.Rows[i].Cells[4].FindControl("FileUpload1");
                string p = "~/proimg/" + fl.FileName;
            fl.SaveAs(MapPath(p));
            TextBox txtdes = (TextBox)GridView1.Rows[i].Cells[5].FindControl("TextBox3");
            TextBox txtsta = (TextBox)GridView1.Rows[i].Cells[6].FindControl("TextBox4");
            string up = "update product_ta set product_price='" + txtpri.Text + "',product_stock='" + txtsto.Text + "',Product_Image='" + p + "',product_description='" + txtdes.Text + "',product_status='" + txtsta.Text + "' where Product_id="+getid+"";
            int j = obj.Fn_Nonquery(up);
            GridView1.EditIndex = -1;
            gridbind_fn();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            gridbind_fn();
        }
    }
}