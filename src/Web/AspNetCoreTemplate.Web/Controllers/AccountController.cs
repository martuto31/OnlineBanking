namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : BaseController
    {
        private readonly IRepository<Account> accountsRepository;
        private readonly IRepository<DebitCard> debitCardsRepository;

        // private readonly SignInManager signInManager
        public AccountController(
            IRepository<Account> accountsRepository,
            IRepository<DebitCard> debitCardsRepository)
        {
            this.accountsRepository = accountsRepository;
            this.debitCardsRepository = debitCardsRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var model = new Account();
            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(Account account)
        {
            if (this.ModelState.IsValid)
            {
                if (!this.accountsRepository.All().Any(x => x.Username == account.Username))
                {
                    await this.accountsRepository.AddAsync(account);
                    await this.accountsRepository.SaveChangesAsync();
                    this.HttpContext.Session.SetString("username", account.Username);
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Потребителското име вече е заето");

                    return this.View();
                }
            }

            return this.View();
        }

        public IActionResult Login()
        {
            var model = new Account();
            return this.View(model);
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (this.ModelState.IsValid)
            {
                if (this.accountsRepository.All().Any(x => x.Username == username))
                {
                    var selectedUser = this.accountsRepository.All()
                        .FirstOrDefault(x => x.Username == username);

                    if (password == selectedUser.Password)
                    {
                        this.HttpContext.Session.SetString("username", username);
                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, "Грешно потребителско име или парола");
                        return this.View();
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Грешно потребителско име или парола");
                    return this.View();
                }
            }
            else
            {
                return this.View();
            }
        }

        public IActionResult AddDebitCard()
        {
            var model = new DebitCard();
            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddDebitCard(DebitCard debitCard)
        {
            if (this.ModelState.IsValid)
            {
                var loggedUser = this.HttpContext.Session.GetString("username");

                var currLoggedUser = this.accountsRepository.All()
                    .FirstOrDefault(x => x.Username == loggedUser);

                var card = new DebitCard()
                {
                    Account = currLoggedUser,
                    CardBalance = 0.00f,
                    CardNumber = debitCard.CardNumber,
                    ExpirationDate = debitCard.ExpirationDate,
                    CreatedOn = DateTime.UtcNow,
                    Currency = debitCard.Currency,
                    CVCCode = debitCard.CVCCode,
                    Id = debitCard.Id,
                };

                await this.debitCardsRepository.AddAsync(card);
                await this.debitCardsRepository.SaveChangesAsync();
            }

            return this.View();
        }
    }
}
