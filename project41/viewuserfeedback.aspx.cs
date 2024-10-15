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
    public partial class viewuserfeedback : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fn_Gridbind();
            }
        }
        public void Fn_Gridbind()
        {
           string sel= "SELECT dbo.User_de.User_Email, dbo.Feedback_Tab.FeedMsg,dbo.Feedback_Tab.FeedId, dbo.User_de.User_Name FROM dbo.Feedback_Tab INNER JOIN dbo.User_de ON dbo.Feedback_Tab.User_Id = dbo.User_de.User_Id and feedstatus=0";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            Session["getid"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("Replytofeedback.aspx");
        }
    }
}