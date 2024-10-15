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
    public partial class productdetview : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
            string sel = "select * from product_ta where product_id=" +Session["productid"]+ "";
            SqlDataReader dr = obj.Fn_Reader(sel);
                while (dr.Read())
                {
                    Label1.Text = dr["Product_Name"].ToString();
                    Label2.Text = dr["Product_Price"].ToString();
                    Label4.Text = dr["Product_Description"].ToString();
                    Image1.ImageUrl = dr["Product_Image"].ToString();

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(cart_id) from cart_tab";
                string i = obj.Fn_Scalar(sel);
            int cartid = 0;
            if(i=="")
            {
                cartid = 1;
            }
            else
            {
                int newcartid = Convert.ToInt32(i);
                cartid = newcartid + 1;
            }
            decimal ppr = Convert.ToDecimal(Label2.Text);
            decimal q = Convert.ToDecimal(TextBox1.Text);
            string sel1="select product_Stock from product_ta where product_id="+Session["productid"]+"";
            string s = obj.Fn_Scalar(sel1);
            decimal sto = Convert.ToDecimal(s);
            if (q <= sto)
            {
                decimal price = ppr * q * 4;
                string ins = "insert into cart_tab values(" + cartid + "," + Session["uid"] + "," + Session["productid"] + "," + q + "," + price + ",'" + DateTime.Now.ToShortDateString() + "')";
                int j = obj.Fn_Nonquery(ins);
                if(j==1)
                {
                    Label6.Visible = true;
                    Label6.Text = "Product add to cart";
                }
            }
            else
            {
                Label6.Visible = true;
                Label6.Text = "Insufficient Stock";
            }
        }
    }
}