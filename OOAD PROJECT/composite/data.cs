using OOAD_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.composite
{


    //public interface Order1
    //{
    //    object execute();
    //}
    //public class Stock
    //{
    //    ProjectContext db = new ProjectContext();
    //    public object Activityshow()
    //    {
    //        return db.ActivityLogs.ToList();
    //    }
    //    public object Loginshow()
    //    {
    //        return db.ActivityLogs.ToList();
    //    }
    //}
    //public class ActivityLog : Order1
    //{
    //    private Stock abcStock;
    //    public ActivityLog(Stock abcStock)
    //    {
    //        this.abcStock = abcStock;
    //    }
    //    public object execute()
    //    {
    //        return abcStock.Activityshow();
    //    }
    //}
    //public class Loginall : Order1
    //{
    //    private Stock abcStock;
    //    public Loginall(Stock abcStock)
    //    {
    //        this.abcStock = abcStock;
    //    }
    //    public object execute()
    //    {
    //        return abcStock.Loginshow();
    //    }
    //}
    //public class Broker
    //{
    //    List<Order> orderList = new List<Order>();
    //    public object takeOrder(Order order)
    //    {
    //        orderList.Add(order);
    //    }
    //    public void placeOrders()
    //    {
    //        foreach(Order1 order in orderList)
    //        {
    //            order.execute();
    //        }
    //        orderList.Clear();
    //    }
    //}
    //class data
    //{
    //    public object login;
    //    public List<data> preReq = new List<data>();
    //}

    //public interface data
    //{
    //     details detail();
    //}
    //public interface details
    //{
    //     object detail();
    //} 
    //public class loginDetails : details
    //{
    //    ProjectContext db = new ProjectContext();

    //    public object detail()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public object details()
    //    {
    //        var data = db.LoginAlls.ToList();
    //        return data;
    //    }
    //}
    //public abstract class getlogin
    //{
    //    public details details()
    //    {
    //        return new loginDetails();
    //    }
    //}
    //public class login
    //{
    //    private List<data> items = new List<data>();
    //    public void addItem(data item)
    //    {
    //        items.Add(item);
    //    }
    //}
    //public class loginbuilder
    //{
    //    public login preparelogin()
    //    {
    //        login l = new login();
    //        l.addItem(new );
    //        return l;
    //    }
    //}
}