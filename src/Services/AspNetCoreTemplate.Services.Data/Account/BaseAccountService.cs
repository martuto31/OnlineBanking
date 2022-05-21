namespace AspNetCoreTemplate.Services.Data.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class BaseAccountService : IBaseAccountService
    {
        private readonly IRepository<Account> accountsRepository;
        private readonly IRepository<DebitCard> debitCardsRepository;
        private readonly IRepository<Transactions> transactionsRepository;

        public BaseAccountService(
            IRepository<Account> accountsRepository,
            IRepository<DebitCard> debitCardsRepository,
            IRepository<Transactions> transactionsRepository)
        {
            this.accountsRepository = accountsRepository;
            this.debitCardsRepository = debitCardsRepository;
            this.transactionsRepository = transactionsRepository;
        }

        public virtual Account GetAccount(string username)
        {
            var selectedAccount = this.accountsRepository.All()
                .FirstOrDefault(x => x.Username == username);

            return selectedAccount;
        }

        public Account GetAccountByIban(string iban)
        {
            var selectedAccount = this.accountsRepository.All()
                .FirstOrDefault(x => x.IBAN == iban);

            return selectedAccount;
        }

        public virtual bool CheckIfAccountExist(string username)
        {
            bool exist = this.accountsRepository.All()
                .Any(x => x.Username == username);

            return exist;
        }

        public bool IsCardNumberUnique(string debitCardNumber)
        {
            bool isUnique = this.debitCardsRepository.All()
                .Any(x => x.CardNumber == debitCardNumber);

            // returns the opposite of the isUnique value because if that card number already exists, it will return true which will make isUnique true when it is not.
            return !isUnique;
        }

        public virtual IEnumerable<TViewModel> GetAccDebitCards<TViewModel>(Account account)
        {
            var debitCards = this.debitCardsRepository.All()
                .Where(d => d.Account == account)
                .To<TViewModel>();

            return debitCards;
        }

        public virtual IEnumerable<TViewModel> GetAllTransactions<TViewModel>(DebitCard debitCard)
        {
            var transactions = this.transactionsRepository.All()
                .Where(d => d.DebitCard == debitCard)
                .To<TViewModel>();

            return transactions;
        }

        public virtual DebitCard GetDebitCard(int id)
        {
            var debitCard = this.debitCardsRepository.All()
                .FirstOrDefault(d => d.Id == id);

            return debitCard;
        }

        public DebitCard GetDebitCard(DebitCard debitCardOfSender)
        {
            var cardOfSender = this.debitCardsRepository.All()
                .Where(d => d.CardNumber == debitCardOfSender.CardNumber &&
                    d.ExpirationDate == debitCardOfSender.ExpirationDate &&
                    d.CVCCode == debitCardOfSender.CVCCode)
                .SingleOrDefault();

            return cardOfSender;
        }

        public bool CheckIfDebitCardExist(DebitCard debitCard)
        {
            var card = this.debitCardsRepository.All()
                 .Where(d => d.CardNumber == debitCard.CardNumber &&
                     d.ExpirationDate == debitCard.ExpirationDate &&
                     d.CVCCode == debitCard.CVCCode)
                 .SingleOrDefault();

            if (card != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
