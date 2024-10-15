using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace project41
{
    public class conectcls
    {
        SqlConnection con;
        
        SqlCommand cmd;
        public conectcls()
        {
            con = new SqlConnection(@"server=DESKTOP-TDAGMA0\SQLEXPRESS;database=assignment4;integrated security=true");
           
        }
        public int Fn_Nonquery(string sql)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sql, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public void Fn_Nonquery_sp(SqlCommand cmd)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public string Fn_Scalar(string sql)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sql, con);
            con.Open();
            string s = cmd.ExecuteScalar().ToString();
            con.Close();
            return s;
        }
        public string Fn_Scalar_sp(SqlCommand cmd)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd.Connection = con;
            con.Open();
            string s = cmd.ExecuteScalar().ToString();
            con.Close();
            return s;
        }
        public SqlDataReader Fn_Reader(string sql)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataSet Fn_Dataset(string sql)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataTable Fn_Datatable(string sql)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return (dt);
        }
    }
}