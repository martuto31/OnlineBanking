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

        public BaseAccountService(
            IRepository<Account> accountsRepository,
            IRepository<DebitCard> debitCardsRepository)
        {
            this.accountsRepository = accountsRepository;
            this.debitCardsRepository = debitCardsRepository;
        }

        public virtual Account GetAccount(string username)
        {
            var selectedAccount = this.accountsRepository.All()
                .FirstOrDefault(x => x.Username == username);

            return selectedAccount;
        }

        public virtual bool CheckIfAccountExist(string username)
        {
            bool exist = this.accountsRepository.All()
                .Any(x => x.Username == username);

            return exist;
        }

        public virtual IEnumerable<TViewModel> GetAccDebitCards<TViewModel>(Account account)
        {
            var debitCards = this.debitCardsRepository.All()
                .Where(d => d.Account == account)
                .To<TViewModel>();

            return debitCards;
        }

        public virtual DebitCard GetDebitCard(int id)
        {
            var debitCard = this.debitCardsRepository.All()
                .FirstOrDefault(d => d.Id == id);

            return debitCard;
        }
    }
}
