using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        private string haskey = "haskey";

        public HashTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(5);
        }

        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            if (db.KeyExists(haskey))
            {
                db.HashGetAll(haskey).ToList().ForEach(x =>
                    {
                        list.Add(x.Name, x.Value);
                    });
            }
            return View(list);
        }


        [HttpPost]
        public IActionResult Add(string name, string value)

        {
            db.HashSet(haskey, name, value);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(string name)
        {
            db.HashDelete(haskey, name);
            return RedirectToAction(nameof(Index));
        }
    }
}
