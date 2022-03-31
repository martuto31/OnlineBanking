namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class AccountController : BaseController
    {
        private readonly IRepository<User> usersRepository;
        private readonly IRepository<DebitCard> debitCardsRepository;

        // private readonly SignInManager signInManager
        public AccountController(
            IRepository<User> usersRepository,
            IRepository<DebitCard> debitCardsRepository)
        {
            this.usersRepository = usersRepository;
            this.debitCardsRepository = debitCardsRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var model = new User();
            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(User user)
        {
            if (this.ModelState.IsValid)
            {
                if (!this.usersRepository.All().Any(x => x.Username == user.Username))
                {
                    await this.usersRepository.AddAsync(user);
                    await this.usersRepository.SaveChangesAsync();
                    this.HttpContext.Session.SetString("username", user.Username);
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
            var model = new User();
            return this.View(model);
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (this.ModelState.IsValid)
            {
                if (this.usersRepository.All().Any(x => x.Username == username))
                {
                    var selectedUser = this.usersRepository.All()
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

            return this.View();
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

                this.usersRepository.All()
                    .FirstOrDefault(x => x.Username == loggedUser)
                    .DebitCards.Add(debitCard);

                await this.usersRepository.SaveChangesAsync();
            }

            return this.View();
        }
    }
}
