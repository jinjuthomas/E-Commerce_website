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
    public partial class viewcart : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gridbind();
                Fn_Total();
            }
        }
        public void gridbind()
        {
            string sel = "select product_ta.product_name,cart_tab.product_quantity,cart_tab.total_price,product_ta.product_image,cart_tab.product_id from product_ta join cart_tab on product_ta.product_id=cart_tab.product_id and user_id="+Session["uid"]+"";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            
        }
        public void Fn_Total()
        {
            string sel = "select sum(Total_Price) from cart_tab where user_id=" + Session["uid"] + " ";
            string tot = obj.Fn_Scalar(sel);
            TextBox2.Text = tot;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string del = "delete from cart_tab where product_id=" + getid + "";
            obj.Fn_Nonquery(del);
            gridbind();
            Fn_Total();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            gridbind();
            Fn_Total();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gridbind();
            Fn_Total();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string sel = "select product_price from product_ta where product_id=" + getid + "";
            string pp = obj.Fn_Scalar(sel);
            Session["price"] = pp;
            TextBox txtquan = (TextBox)GridView1.Rows[i].Cells[3].FindControl("TextBox1");
            decimal j = Convert.ToDecimal(Session["price"]) * Convert.ToDecimal(txtquan.Text) * 4;
            string up = "update cart_tab set product_quantity=" + txtquan.Text + ",total_price=" + j + " where product_id="+getid+"";
            obj.Fn_Nonquery(up);
            GridView1.EditIndex = -1;
            gridbind();
            Fn_Total();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select cart_id from cart_tab where user_id=" + Session["uid"] + "";           
            List<int> lis = new List<int>();
            SqlDataReader dr = obj.Fn_Reader(sel);
            while (dr.Read())
            {
                lis.Add(Convert.ToInt32( dr["Cart_Id"]));

            }
            foreach(int i in lis)
            {
                string sel1 = "select * from cart_tab where (cart_id=" + i + " AND user_id = " + Session["uid"] +")";
                SqlDataReader dr1 = obj.Fn_Reader(sel1);
                int pid = 0;
                decimal qua = 0;
                decimal tp = 0;
                while (dr1.Read())
                {
                    pid = Convert.ToInt32(dr1["Product_Id"]);
                    qua = Convert.ToDecimal(dr1["Product_Quantity"]);
                    tp = Convert.ToInt32(dr1["Total_Price"]);

                }
                string ins = "insert into Order_tab values(" + pid + "," + Session["uid"] + "," + qua + "," + tp + ",'" + DateTime.Now.ToShortDateString() + "','Ordered')";
                int k = obj.Fn_Nonquery(ins);
                string del = "delete from cart_tab where product_id=" + pid + " and user_id=" + Session["uid"] + "";
                int d = obj.Fn_Nonquery(del);
            }
            decimal gt = Convert.ToDecimal(TextBox2.Text);
            string ins1 = "insert into bill_tab values(" + Session["uid"] + "," + gt + ",'" + DateTime.Now.ToShortDateString() + "','Ordered')";
            obj.Fn_Nonquery(ins1);
            Response.Redirect("viewbill.aspx");
        }
    }
}