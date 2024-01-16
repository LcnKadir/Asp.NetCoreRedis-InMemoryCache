using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class Products : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public Products(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //Key değerinin Memory'de olup olmadığını tespit etmek için 1.Yol //1 To determine whether the Key value is in Memory or not.The Road//

            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}

            //Key değerinin Memory'de olup olmadığını tespit etmek için 2.Yol //2 To determine whether the Key value is in Memory or not.The Road//
            //if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}


            //Absolute ve Sliding Expiration' un birlikte kullanımı.////Combined use of Absolute and Sliding Expiration.//
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            options.SlidingExpiration = TimeSpan.FromSeconds(10);

            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue<string>("zaman", out string zamancache);
            ViewBag.zaman = zamancache;

            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
