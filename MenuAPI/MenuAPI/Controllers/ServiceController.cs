using System.Web.Http;
using System.Linq;

namespace MenuAPI.Controllers
{
    public class ServiceController : ApiController
    {
        SystemEntities _context = new SystemEntities();
        
        // GET: Service
        [HttpGet]
        public dynamic Get()
        {
            var json = _context.Restaurants.Where(w=>w.IsActive == true).Select( i => new { i.R_Name, i.R_Image, i.R_OpenTime, i.R_DeliveryTime, i.R_Rating, i.R_Tags });
            return json;
        }
    }
}