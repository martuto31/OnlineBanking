namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Diagnostics;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Account;
    using AspNetCoreTemplate.Web.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IBaseAccountService accountService;

        public HomeController(IBaseAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Index()
        {
            var loggedUser = this.HttpContext.Session.GetString("username");
            if (loggedUser != null)
            {
                var accountModel = this.accountService.GetAccount(loggedUser);
                return this.View(accountModel);
            }
            else
            {
                return this.RedirectToAction("Login", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
