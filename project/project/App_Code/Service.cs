using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	SqlConnection con = new SqlConnection(@"server=DESKTOP-TDAGMA0\SQLEXPRESS;database=Assignment4;integrated security=true");
	public decimal acc_bal(int aacno)
    {
		decimal bal = 0;
		string s = "select balance_amt from account_tab where account_no=" + aacno +" and status='Active' ";
		SqlCommand cmd = new SqlCommand(s, con);
		con.Open();
		SqlDataReader dr = cmd.ExecuteReader();
		while(dr.Read())
        {
			bal = Convert.ToDecimal(dr["balance_amt"]);
			
        }
		con.Close();
		return bal;
    }
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
}
