using OOAD_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Strategy
{
    public interface data
    {
        object getdetails(int id);
    }
    public class customerdetail : data
    {
        ProjectContext db = new ProjectContext();

        public object getdetails(int id)
        {
            return db.Customers.Where(model => model.Customerid == id).FirstOrDefault();
        }
    }
    public class userdetail : data
    {
        ProjectContext db = new ProjectContext();

        public object getdetails(int id)
        {
            return db.LoginAlls.Where(model => model.Id == id).FirstOrDefault();
        }
    }


    public class Context
    {
        private data data;
        public Context(data data)
        {
            this.data = data;
        }
        public object executeStrategy(int id)
        {
            return data.getdetails(id);
        }
    }
    //public abstract class details
    //{
    //    protected data data;

    //    protected details(data data)
    //    {
    //        this.data = data;
    //    }
    //    public abstract object detail();
    //}
    //public class detailbyid : details
    //{
    //    public int id;

    //    public detailbyid(int id, data data) : base(data)
    //    {
    //        this.id = id;
    //    }



    //    public override object detail()
    //    {
           
    //         return data.getdetails(id);
    //    }

    //}
}