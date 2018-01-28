using System.Web.Http;
using System.Linq;
using System;
using System.Net.Http;

namespace MenuAPI.Controllers
{
    public class ServiceController : ApiController
    {
        SystemEntities _context = new SystemEntities();

        // GET: Service
        [HttpGet]
        public dynamic Get()
        {
            var json = _context.Restaurants.Where(w => w.IsActive == true).Select(i => new { i.RestaurantID, i.R_Name, i.R_Image, i.R_OpenTime, i.R_DeliveryTime, i.R_Rating, i.R_Tags });
            return json;
        }

        [HttpPost]
        public dynamic GetItems(int id)
        {
            int Rids = Convert.ToInt32(id);
            if(Rids != 0)
            {
                int RestaurantID = Convert.ToInt32(id);
                var json = _context.MenuItems.Where(w => w.IsActive == true && w.RestaurantID == RestaurantID).Select(i => new { i.ItemID, i.RestaurantID , i.ItemName, i.Price, i.Description, i.ItemImage });

                return json;
            }
            
            return "null";
        }       
    }
}