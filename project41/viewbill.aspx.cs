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
    public partial class viewbill : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sel1 = "select grant_total from bill_tab where user_id=" + Session["uid"] + "and bill_status='Ordered'";
                Label18.Text = obj.Fn_Scalar(sel1);
                string sel = "select bill_id,date from bill_tab where user_id=" + Session["uid"] + " and bill_status='Ordered'";
                SqlDataReader dr = obj.Fn_Reader(sel);
                while (dr.Read())
                {
                    Label14.Text = dr["Bill_Id"].ToString();
                    Label16.Text = Convert.ToDateTime(dr["Date"]).ToString("dd/MM/yyyy");
                }
                gridbind_fn();
            }
        }
        public void gridbind_fn()
        {

            string sel = "SELECT dbo.Product_ta.Product_Name, dbo.Order_tab.Product_Quantity, dbo.Order_tab.Total_Price FROM dbo.Order_tab INNER JOIN dbo.Product_ta ON dbo.Order_tab.Product_Id = dbo.Product_ta.Product_Id where user_id=" + Session["uid"] + " and order_tab.order_status='Ordered'";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string sel = "select order_id from order_tab where user_id=" + Session["uid"] + " and order_status='Ordered'";
            List<int> lis = new List<int>();
            SqlDataReader dr = obj.Fn_Reader(sel);
            while (dr.Read())
            {
                lis.Add(Convert.ToInt32(dr["Order_Id"]));

            }
            foreach (int i in lis)
            {
                string sel1 = "select * from order_tab where (order_id=" + i + " AND user_id = " + Session["uid"] + " and order_status='ordered')";
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
                string up = "update order_tab set order_status='Cancelled' where User_id=" + Session["uid"] + " and order_id="+i+"";
                obj.Fn_Nonquery(up);
            }
            string sel2 = "select max(bill_id) from bill_tab where user_id=" + Session["uid"] + " ";
            string mid = obj.Fn_Scalar(sel2);
            int bid = Convert.ToInt32(mid);
            string up1 = "update bill_tab set bill_status='Cancelled' where bill_id="+bid+"";
            obj.Fn_Nonquery(up1);
            Response.Redirect("Userprof.aspx");
        }
    }
}