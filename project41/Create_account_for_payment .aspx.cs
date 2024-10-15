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
    public partial class Create_account_for_payment : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_accdet";
            cmd.Parameters.AddWithValue("@uid", Session["uid"]);
            cmd.Parameters.AddWithValue("@accno", TextBox1.Text);
            cmd.Parameters.AddWithValue("@accty", TextBox2.Text);
            cmd.Parameters.AddWithValue("@accbl", TextBox3.Text);
            cmd.Parameters.AddWithValue("@status", "Active");
            SqlParameter sp = new SqlParameter();
            sp.DbType = DbType.Int32;
            sp.ParameterName = "@sta";
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            obj.Fn_Nonquery_sp(cmd);
            int i = Convert.ToInt32(sp.Value);
            if (i == 1)
            {
                Response.Redirect("payment.aspx");
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string sel_text = "select count(account_no) from account_tab where account_no="+TextBox1.Text+"";
            string i = obj.Fn_Scalar(sel_text);
            int j = Convert.ToInt32(i);
            if(j>=1)
            {
                Label6.Visible = true;
                Label6.Text = "Account number already exist please update wallet";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            string bal_amt = "select balance_amt from account_tab where user_id=" + Session["uid"] + "";
            string j = obj.Fn_Scalar(bal_amt);
            Label7.Text = j;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string bal_amt = "select balance_amt from account_tab where user_id=" + Session["uid"] + "";
            string j = obj.Fn_Scalar(bal_amt);
            decimal bal = Convert.ToDecimal(j);
            decimal newbal = bal + Convert.ToDecimal(TextBox4.Text);
            string up_bal = "update account_tab set balance_amt=" + newbal + " where user_id=" + Session["uid"] + "";
            int i = obj.Fn_Nonquery(up_bal);
            if (i == 1)
            {
                Label11.Visible = true;
                Label11.Text = "Balance Updated Continue Shopping";

            }
        }
    }
}