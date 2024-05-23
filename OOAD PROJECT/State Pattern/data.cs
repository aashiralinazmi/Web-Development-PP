using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.State_Pattern
{
    public interface data
    {
        DataSet DoAction();
    }
    public class getvieworder : data
    {
        public DataSet DoAction()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);


            SqlCommand com = new SqlCommand("select Invoices.Id, Invoices.UserId, LoginAlls.Id, LoginAlls.Username as 'user', Invoices.TotalPrice, Invoices.Payment, Invoices.Date, Invoices.Status from Invoices inner join LoginAlls on LoginAlls.id = Invoices.UserId", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;
        }
    }
    public class context
    {
        private data d;

        public context()
        {
            d = null;
        }
        public DataSet setState(data d)
        {
            this.d = d;
            return this.d.DoAction();
        }
        public data getState()
        {
            return d;
        }
    }
}