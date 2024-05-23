﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

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
            SqlCommand com = new SqlCommand("select top 16 * from Products order by Id DESC", con);
        }
    }

    public class MenWearProducts : data //concrete product, child of data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet Data()
        {
            SqlCommand com = new SqlCommand("select top 12 * from Products where Category like 'M%' order by Id DESC", con);
        }
    }

    public class FemaleWearProducts : data //concrete product, child of data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet Data()
        {
            SqlCommand com = new SqlCommand("select top 16 * from Products where Category like 'F%' order by Id DESC", con);
        }
    }



    //Factory Pattern end


    /*public class data
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectContext"].ConnectionString);
        public DataSet ProductsIndex()
        public DataSet MenWearProducts()
        public DataSet FemaleWearProducts()
        public DataSet getvieworder()
        public DataSet getvieworder(int id)
    }*/
}