using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        private string listkey = "names";
        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }

        public IActionResult Index()
        {
            List<string> namelist = new List<string>();

            if (db.KeyExists(listkey))
            {
                db.ListRange(listkey).ToList().ForEach(x =>
                {
                    namelist.Add(x.ToString());
                });
            }
            return View(namelist);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            db.ListRightPush(listkey, name);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(string name)
        {
            db.ListRemoveAsync(listkey,name).Wait();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteFirstList()
        {
            db.ListLeftPop(listkey);
           //db.ListRightPop(listkey);
            return RedirectToAction("Index");

        }

    }
}
