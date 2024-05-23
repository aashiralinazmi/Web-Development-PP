using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;using System.Data;using System.Configuration;using OOAD_PROJECT.Models;

namespace OOAD_PROJECT.DataBaseAccessLayer
{
 
    //Factory Pattern start
    public interface data // abstract product
    {
        DataSet Data();
    }

    public class dataFactory //layer , concrete factory
    {
        public data getData(String dataType)
        {

            if (dataType == "products")
            {
                return new ProductsIndex();
            }
            else if (dataType == "men")
            {
                return new MenWearProducts();
            }
            else if (dataType == "women")
            {
                return new FemaleWearProducts();
            }

            return null;
        }
    }

    public class ProductsIndex : data //concrete product, child of data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet Data()
        {
            SqlCommand com = new SqlCommand("select top 16 * from Products order by Id DESC", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;
        }
    }

    public class MenWearProducts : data //concrete product, child of data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet Data()
        {
            SqlCommand com = new SqlCommand("select top 12 * from Products where Category like 'M%' order by Id DESC", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;
        }
    }

    public class FemaleWearProducts : data //concrete product, child of data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet Data()
        {
            SqlCommand com = new SqlCommand("select top 16 * from Products where Category like 'F%' order by Id DESC", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;
        }
    }



    //Factory Pattern end


    /*public class data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet ProductsIndex()        {            SqlCommand com = new SqlCommand("select top 16 * from Products order by Id DESC", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;        }
        public DataSet MenWearProducts()        {            SqlCommand com = new SqlCommand("select top 12 * from Products where Category like 'M%' order by Id DESC", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;        }
        public DataSet FemaleWearProducts()        {            SqlCommand com = new SqlCommand("select top 16 * from Products where Category like 'F%' order by Id DESC", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;        }
        public DataSet getvieworder()        {            SqlCommand com = new SqlCommand("select Invoices.Id, Invoices.UserId, LoginAlls.Id, LoginAlls.Username as 'user', Invoices.TotalPrice, Invoices.Payment, Invoices.Date, Invoices.Status from Invoices inner join LoginAlls on LoginAlls.id = Invoices.UserId", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;        }
        public DataSet getvieworder(int id)        {            SqlCommand com = new SqlCommand("select Invoices.Id, Invoices.UserId, LoginAlls.Id, LoginAlls.Username as 'user', Invoices.TotalPrice, Invoices.Payment, Invoices.Date, Invoices.Status from Invoices inner join LoginAlls on LoginAlls.id = Invoices.UserId where Invoices.Id =" + id + "", con);            SqlDataAdapter da = new SqlDataAdapter(com);            DataSet ds = new DataSet();            da.Fill(ds);            return ds;        }
    }*/
}