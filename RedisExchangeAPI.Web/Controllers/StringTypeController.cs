using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {

        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDb(0);

            db.StringSet("name", "Kadir Liçina");
            db.StringSet("ziyaretçi", 100);


            return View();
        }

        public IActionResult Show()
        {
            var name = db.StringLength("name");

            //db.StringIncrement("ziyaretçi", 10); 

            //var count = db.StringDecrementAsync("ziyaretçi", 1).Result;

            //db.StringDecrementAsync("ziyaretçi", 10).Wait();

            ViewBag.Name = name.ToString();

            return View();
        }
    }
}
