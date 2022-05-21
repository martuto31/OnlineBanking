namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Account;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : BaseController
    {
        private readonly IRepository<Account> accountsRepository;
        private readonly IRepository<DebitCard> debitCardsRepository;
        private readonly IRepository<Transactions> transactionRepository;
        private readonly IBaseAccountService accountService;

        public AccountController(
            IRepository<Account> accountsRepository,
            IRepository<DebitCard> debitCardsRepository,
            IBaseAccountService accountService,
            IRepository<Transactions> transactionRepository)
        {
            this.accountsRepository = accountsRepository;
            this.debitCardsRepository = debitCardsRepository;
            this.transactionRepository = transactionRepository;
            this.accountService = accountService;
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
                if (!this.accountService.CheckIfAccountExist(account.Username))
                {
                    Random random = new Random();
                    random.Next(0000, 9999);
                    var acc = new Account()
                    {
                        AccountBalance = 0.00f,
                        Username = account.Username,
                        Password = account.Password,
                        ConfirmPassword = account.ConfirmPassword,
                        DebitCards = account.DebitCards,
                        Address = account.Address,
                        Age = account.Age,
                        Email = account.Email,
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        Id = account.Id,
                        ModifiedOn = account.ModifiedOn,
                        IBAN = "BG" + random.Next(0000, 9999) + "GLIGI" + random.Next(00000000, 99999999),
                        CreatedOn = account.CreatedOn,
                        Currency = account.Currency,
                    };

                    await this.accountsRepository.AddAsync(acc);
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
                if (this.accountService.CheckIfAccountExist(username))
                {
                    var selectedUser = this.accountService.GetAccount(username);

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

        public IActionResult Logout()
        {
            // Removes the username of the session
            var loggedUser = this.HttpContext.Session.GetString("username");
            if (loggedUser != null)
            {
                this.HttpContext.Session.Remove("username");
                return this.RedirectToAction("Login", "Account");
            }

            return this.RedirectToAction("Login", "Account");
        }

        public IActionResult AddDebitCard()
        {
            var model = new DebitCard();
            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddDebitCardAsync(DebitCard debitCard)
        {
            if (this.ModelState.IsValid)
            {
                var loggedUser = this.HttpContext.Session.GetString("username");
                if (loggedUser != null)
                {
                    Random random = new Random();

                    var currLoggedUser = this.accountService.GetAccount(loggedUser);

                    var card = new DebitCard()
                    {
                        Account = currLoggedUser,
                        IBAN = "BG" + random.Next(0000, 9999) + "GLIGI" + random.Next(00000000, 99999999),
                        CardBalance = 200,
                        CardNumber = debitCard.CardNumber,
                        ExpirationDate = debitCard.ExpirationDate,
                        CreatedOn = DateTime.UtcNow,
                        Currency = debitCard.Currency,
                        CVCCode = debitCard.CVCCode,
                        Id = debitCard.Id,
                    };

                    await this.debitCardsRepository.AddAsync(card);
                    await this.debitCardsRepository.SaveChangesAsync();
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    return this.RedirectToAction("Login", "Account");
                }
            }

            return this.View();
        }

        public IActionResult AddFunds()
        {
            var model = new DebitCard();
            return this.View(model);
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddFundsAsync(DebitCard debitCardOfSender, int id, int amountOfFunds)
        {
            // id stands for the id of the receiver's debit card
            var cardOfSender = this.accountService.GetDebitCard(debitCardOfSender);
            var cardOfReceiver = this.accountService.GetDebitCard(id);

            // Checks if the the card exists.
            if (cardOfSender != null)
            {
                // Checks whether the sender's card has enough amount of funds.
                if (cardOfSender.CardBalance >= amountOfFunds)
                {
                    // Creating transaction for the debit card of the sender
                    Transactions transactionForSender = new Transactions()
                    {
                        Payment = amountOfFunds,
                        Receipt = 0,
                        CreatedOn = DateTime.UtcNow,
                        Date = DateTime.UtcNow,
                        DebitCard = cardOfSender,
                        Message = "Transfer of funds",
                        TransactionCurrency = debitCardOfSender.Currency,
                    };

                    Transactions transactionForReceiver = new Transactions()
                    {
                        Payment = 0,
                        Receipt = amountOfFunds,
                        CreatedOn = DateTime.UtcNow,
                        Date = DateTime.UtcNow,
                        DebitCard = cardOfReceiver,
                        Message = "Transfer of funds",
                        TransactionCurrency = debitCardOfSender.Currency,
                    };

                    cardOfSender.CardBalance -= amountOfFunds;
                    cardOfReceiver.CardBalance += amountOfFunds;

                    await this.transactionRepository.AddAsync(transactionForSender);
                    await this.transactionRepository.AddAsync(transactionForReceiver);

                    this.debitCardsRepository.Update(cardOfSender);
                    this.debitCardsRepository.Update(cardOfReceiver);

                    await this.transactionRepository.SaveChangesAsync();
                    await this.debitCardsRepository.SaveChangesAsync();

                    return this.Ok("Succesfull transaction");
                }
                else
                {
                    return this.Problem("Not enough funds in card.");
                }
            }
            else
            {
                return this.BadRequest("Incorrect debit card information.");
            }
        }
    }
}
