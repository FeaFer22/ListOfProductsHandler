using ListOfProductsHandler.Database.Context;
using ListOfProductsHandler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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
