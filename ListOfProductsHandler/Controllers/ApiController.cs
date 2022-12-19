using ListOfProductsHandler.Database.Context;
using Microsoft.AspNetCore.Mvc;

namespace ListOfProductsHandler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly AppDbContext _context;
        public ApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetList()
        {
            return Json(_context.Products);
        }
    }
}
