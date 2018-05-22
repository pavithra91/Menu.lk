using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuAPI.App_Code
{
    public class Order
    {
        public string OrderID { get; set; }
        public string Name { get; set; }
        public string TelephoneNo { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }        
        public List<OrderItem> ItemList { get; set; }
    }

    public class OrderItem
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public int qty { get; set; }
        public string SpecialNote { get; set; }
    }
}