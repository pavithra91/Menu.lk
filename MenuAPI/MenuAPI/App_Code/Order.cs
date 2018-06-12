using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuAPI.App_Code
{
    public class Order
    {
        public string OrderID { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string notes { get; set; }        
        public List<IMenuItem> ItemList { get; set; }
    }

    public class IMenuItem
    {
        public string _id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string price { get; set; }
        public string userNotes { get; set; }
    }
}