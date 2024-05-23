using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;using System.Data;using System.Configuration;

namespace OOAD_PROJECT.Singleton
{
    public class data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
    
        public static data instance = new data();
        public data() { }
        public static data getInstance()
        {
            return instance;
        }

        public DataSet getvieworder(int id)
            {
                SqlCommand com = new SqlCommand("select Invoices.Id, Invoices.UserId, LoginAlls.Id, LoginAlls.Username as 'user', Invoices.TotalPrice, Invoices.Payment, Invoices.Date, Invoices.Status from Invoices inner join LoginAlls on LoginAlls.id = Invoices.UserId where Invoices.Id =" + id + "", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
    }
}