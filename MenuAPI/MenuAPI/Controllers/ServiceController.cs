using System.Web.Http;
using System.Linq;
using System;
using System.Net.Http;
using MenuAPI.App_Code;

namespace MenuAPI.Controllers
{
    public class ServiceController : ApiController
    {
        SystemEntities _context = new SystemEntities();

        // GET: Service
        [HttpGet]
        public dynamic Get()
        {
            var json = _context.Restaurants.Where(w => w.IsActive == true).Select(i => new { _id= i.RestaurantID, name=i.R_Name, imageURL=i.R_Image, workingTime=i.R_OpenTime,takeaway= i.R_DeliveryTime,rating= i.R_Rating, category= i.R_Tags });
            return json;
        }

        [HttpGet]
        public dynamic GetItems(int id)
        {
            int Rids = Convert.ToInt32(id);
            if(Rids != 0)
            {
                int RestaurantID = Convert.ToInt32(id);
                var json = _context.MenuItems.Where(w => w.IsActive == true && w.RestaurantID == RestaurantID).Select(i => new { _id = i.ItemID, name = i.ItemName, price = i.Price, description = i.Description, imageURL = i.ItemImage, type = i.ItemCategoty.Name, subhead = i.ItemCategoty.SubHeading });

                return json;
            }
            
            return "null"; 
        }
        
        [HttpPost]
        public dynamic PlaceOrder(Order _order)
        {
            try
            {
                Customer cus = _context.Customers.Where(w => w.Email == _order.email).FirstOrDefault();
                int CustomerID = 0;
                if (cus == null)
                {
                    Customer _objCus = new Customer();
                    _objCus.Name = _order.name;
                    _objCus.Email = _order.email;
                    _objCus.Address = _order.address;
                    _objCus.TelephoneNo = _order.tel;

                    _context.Customers.Add(_objCus);
                    _context.SaveChanges();

                    CustomerID = _objCus.CustomerID;
                }
                else
                {
                    CustomerID = cus.CustomerID;
                }

                CustomerOrder _cusOrder = new CustomerOrder();
                _cusOrder.CustomerID = CustomerID;
                _context.CustomerOrders.Add(_cusOrder);
                _context.SaveChanges();

                foreach (var items in _order.ItemList)
                {
                    ShoppingCart cusOrder = new ShoppingCart();
                    cusOrder.ItemID = Convert.ToInt32(items._id);
                    cusOrder.Qty = Convert.ToInt32(items.quantity);
                    cusOrder.Price = Convert.ToDouble(items.price);
                    cusOrder.ItemNote = items.userNotes;

                    _context.ShoppingCarts.Add(cusOrder);
                }

                return "Your Order Place Sucessfully. Order ID : " + _cusOrder.OrderID;
            }
            catch(Exception ex)
            {
                return "Error. Error Msg : " + ex.Message;
            }
        }       
    }
}