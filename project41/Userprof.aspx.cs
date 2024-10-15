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
    public partial class Userprof : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sel = "select * from category";
                DataTable dt = obj.Fn_Datatable(sel);
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int getid = Convert.ToInt32(e.CommandArgument);
            Session["categoryid"] = getid;
            Response.Redirect("producthome.aspx");
        }
    }
}