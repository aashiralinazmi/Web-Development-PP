using OOAD_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Facade_Pattern
{
    public interface data
    {
        object getData();
    }
    public class Index : data
    {
        ProjectContext db = new ProjectContext();
        public object getData()
        {
            var data = db.Product.ToList();
            return data;
        }
       
    }
    public class Orders : data
    {
        ProjectContext db = new ProjectContext();
        public object getData()
        {
            var data = db.orders.ToList();
            return data;
        }
    }
    public class detailGetter
    {
        public Index index;
        public Orders orders;

        public detailGetter()
        {
            index = new Index();
            orders = new Orders();
        }
        public object getIndex()
        {
            return index.getData();
        }
        public object getOrder()
        {
            return orders.getData();
        }
    }
}