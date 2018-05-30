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
            Customer cus = new Customer();
            cus.Email = "Test"; //_order.Email;
            cus.Name =  _order.name;

            _context.Customers.Add(cus);

            CustomerOrder cusOrder = new CustomerOrder();
            cusOrder.CustomerID = cus.CustomerID;
            _context.CustomerOrders.Add(cusOrder);

            _context.SaveChanges();

            return "Test Message Sudu malli " + _order.name;
        }       
    }
}