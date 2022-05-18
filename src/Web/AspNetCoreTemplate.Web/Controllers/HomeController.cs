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
                var account = this.accountService.GetAccount(loggedUser);
                var debitCards = this.accountService.GetAccDebitCards<DebitCard>(account);

                AccountViewModel accountModel = new AccountViewModel()
                {
                    Account = account,
                    DebitCards = debitCards,
                };
                return this.View(accountModel);
            }
            else
            {
                return this.RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Information(int id)
        {
            var loggedUser = this.HttpContext.Session.GetString("username");
            if (loggedUser != null)
            {
                var account = this.accountService.GetAccount(loggedUser);
                var debitCard = this.accountService.GetDebitCard(id);

                DebitCardViewModel accountModel = new DebitCardViewModel()
                {
                    Account = account,
                    DebitCard = debitCard,
                };

                return this.View(accountModel);
            }
            else
            {
                return this.RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Transactions(int id)
        {
            var loggedUser = this.HttpContext.Session.GetString("username");
            if (loggedUser != null)
            {
                var debitCard = this.accountService.GetDebitCard(id);
                var transactions = this.accountService.GetAllTransactions<Transactions>(debitCard);

                TransactionsViewModel transactionsViewModel = new TransactionsViewModel()
                {
                    DebitCard = debitCard,
                    Transactions = transactions,
                };

                return this.View(transactionsViewModel);
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
