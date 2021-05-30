using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AsyncAwait.Task2.CodeReviewChallenge.Models;
using AsyncAwait.Task2.CodeReviewChallenge.Models.Support;
using AsyncAwait.Task2.CodeReviewChallenge.Services;

namespace AsyncAwait.Task2.CodeReviewChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAssistant _assistant;

        private readonly IPrivacyDataService _privacyDataService;

        public HomeController(IAssistant assistant, IPrivacyDataService privacyDataService)
        {
            _assistant = assistant ?? throw new ArgumentNullException(nameof(assistant));
            _privacyDataService = privacyDataService ?? throw new ArgumentNullException(nameof(privacyDataService));
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Privacy()
        {
            /* 
            sync over async - ситуация
            лучше избегать, так как блокируем входящий поток и может возникнуть dead lock
            also: get Aggregate exception
            */
            //ViewBag.Message = _privacyDataService.GetPrivacyDataAsync().Result;
            ViewBag.Message = await _privacyDataService.GetPrivacyDataAsync();
            return View();
        }

        public async Task<IActionResult> Help()
        {
            /* 
            if you’re writing app-level code, do not use ConfigureAwait(false)
            ConfigureAwait определяет, следует ли возобновлять работу на захваченном SynchronizationContext или нет.
            В приложениях asp.net ресурс SynchronizationContext является контекстом запроса
            if use ConfigureAwait(false) we set ViewBag.RequestInfo in different thread and we try to modify component created in controller thread
            */
            //ViewBag.RequestInfo = await _assistant.RequestAssistanceAsync("guest").ConfigureAwait(false);
            ViewBag.RequestInfo = await _assistant.RequestAssistanceAsync("guest");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
