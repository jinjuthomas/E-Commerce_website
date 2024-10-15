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

    public partial class payment : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sel1 = "select grant_total from bill_tab where user_id=" + Session["uid"] + " and bill_status='Ordered'";
            Session["tot"] = obj.Fn_Scalar(sel1);
            Label2.Text = Session["tot"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel_acc = "select account_no from account_tab where user_id=" + Session["uid"] + "";
            string accno = obj.Fn_Scalar(sel_acc);
            int accnum = Convert.ToInt32(accno);
            if (accnum == Convert.ToInt32(TextBox1.Text))
                {
                balance_amtn.ServiceClient obj1 = new balance_amtn.ServiceClient();
                decimal bal = obj1.acc_bal(Convert.ToInt32(TextBox1.Text));
                decimal gt = Convert.ToDecimal(Session["tot"]);
                if (bal >= gt)
                {
                    decimal newbal = bal - gt;
                    string up = "update account_tab set balance_amt=" + newbal + " where account_no=" + Convert.ToInt32(TextBox1.Text) + "";
                    int i = obj.Fn_Nonquery(up);
                    if (i == 1)
                    {
                        string sel = "select order_id from order_tab where user_id=" + Session["uid"] + " and order_status='Ordered'";
                        List<int> olis = new List<int>();
                        SqlDataReader dr2 = obj.Fn_Reader(sel);
                        while (dr2.Read())
                        {
                            olis.Add(Convert.ToInt32(dr2["Order_Id"]));
                        }
                        foreach (int k in olis)
                        {
                            string up1 = "update order_tab set order_status='Paid' where order_id=" + k + "";
                            obj.Fn_Nonquery(up1);
                        }
                        string sel1 = "select max(bill_id) from bill_tab where user_id=" + Session["uid"] + " ";
                        string bid = obj.Fn_Scalar(sel1);
                        string up2 = "update bill_tab set bill_status='Paid' where bill_id=" + bid + "";
                        obj.Fn_Nonquery(up2);
                        string sel2 = "select product_id from order_tab where order_status='Paid' and user_id=" + Session["uid"] + "";
                        List<int> plis = new List<int>();
                        SqlDataReader dr = obj.Fn_Reader(sel2);
                        while (dr.Read())
                        {
                            plis.Add(Convert.ToInt32(dr["Product_Id"]));
                        }
                        foreach (int j in plis)
                        {
                            string sel3 = "SELECT dbo.Product_ta.Product_Stock, dbo.Order_tab.Product_Quantity FROM dbo.Product_ta INNER JOIN dbo.Order_tab ON dbo.Product_ta.Product_Id = dbo.Order_tab.Product_Id where Order_tab.product_id=" + j + " and User_id=" + Session["uid"] + "";
                            SqlDataReader dr1 = obj.Fn_Reader(sel3);
                            decimal ps = 0;
                            decimal qua = 0;
                            while (dr1.Read())
                            {
                                ps = Convert.ToDecimal(dr1["Product_Stock"]);
                                qua = Convert.ToDecimal(dr1["Product_Quantity"]);
                            }
                            decimal newst = ps - qua;
                            string newpst = newst.ToString();
                            string up3 = "update product_ta set product_stock='" + newpst + "' where product_id=" + j + "";
                            int k = obj.Fn_Nonquery(up3);
                            if (k == 1)
                            {
                                Label4.Visible = true;
                                Label4.Text = "Successfully Paid";
                            }
                        }
                    }
                }
                else
                {
                    Label4.Visible = true;
                    Label4.Text = "Insufficient Balance";
                    string sel = "select order_id from order_tab where user_id=" + Session["uid"] + " and order_status='Ordered'";
                    List<int> olis = new List<int>();
                    SqlDataReader dr2 = obj.Fn_Reader(sel);
                    while (dr2.Read())
                    {
                        olis.Add(Convert.ToInt32(dr2["Order_Id"]));
                    }
                    foreach (int k in olis)
                    {
                        string up1 = "update order_tab set order_status='Insuff_bal' where order_id=" + k + "";
                        obj.Fn_Nonquery(up1);
                    }
                    string sel1 = "select max(bill_id) from bill_tab where user_id=" + Session["uid"] + " ";
                    string bid = obj.Fn_Scalar(sel1);
                    string up2 = "update bill_tab set bill_status='Insuff_bal' where bill_id=" + bid + "";
                    obj.Fn_Nonquery(up2);         
                }
            }
            else
            {
                Response.Redirect("Create_account_for_payment.aspx");
            }
        }
    }
}

