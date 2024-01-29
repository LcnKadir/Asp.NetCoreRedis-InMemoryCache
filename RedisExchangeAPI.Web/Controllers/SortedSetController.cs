using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        private string listkey = "sortedsetname";

        public SortedSetController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (db.KeyExists(listkey))
            {
                //db.SortedSetScan(listkey).ToList().ForEach(x =>
                //{
                //    list.Add(x.ToString());
                //});

                //Data büyükten küçüğe doğru sıralanıyor./ Data sorted from largest to smallest. 
                //db.SortedSetRangeByRank(listkey, order: Order.Descending).ToList().ForEach(x =>
                //{ 
                //list.Add(x.ToString());
                //});

                db.SortedSetRangeByRank(listkey, order: Order.Descending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {

            db.SortedSetAdd(listkey, name, score);
            db.KeyExpire(listkey, DateTime.Now.AddMinutes(1));
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(string name)
        {
            db.SortedSetRemove(listkey, name);

            return RedirectToAction(nameof(Index));
        }
    }
}
