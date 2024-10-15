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
    public partial class addproduct : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sel = "select Category_id,category_name from category";
                DataSet ds = obj.Fn_Dataset(sel);
                DropDownList1.DataSource = ds;
                DropDownList1.DataValueField = "Category_Id";
                DropDownList1.DataTextField = "Category_Name";
                DropDownList1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/proimg/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));
            string ins = "insert into product_ta values(" + DropDownList1.SelectedItem.Value + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + p + "','" + TextBox4.Text + "','Available')";
            int i = obj.Fn_Nonquery(ins);
        }
    }
}