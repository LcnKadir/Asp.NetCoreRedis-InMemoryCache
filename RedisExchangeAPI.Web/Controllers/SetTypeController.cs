using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        private string listkey = "hasnames";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }

        public ActionResult Index()
        {
            HashSet<string> namelist = new HashSet<string>();
            if(db.KeyExists(listkey))
            {
                db.SetMembers(listkey).ToList().ForEach(x =>
                {
                    namelist.Add(x.ToString());
                });
            }

            return View(namelist);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            db.KeyExpire(listkey, DateTime.Now.AddMinutes(5));

            db.SetAdd(listkey, name);

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(string name) 
        {
           await db.SetRemoveAsync(listkey, name);

            return RedirectToAction("Index");
        }
    }
}
