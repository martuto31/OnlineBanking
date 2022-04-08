namespace AspNetCoreTemplate.Services.Data.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;

    public class BaseAccountService : IBaseAccountService
    {
        private readonly IRepository<Account> accountsRepository;

        public BaseAccountService(IRepository<Account> accountsRepository)
        {
            this.accountsRepository = accountsRepository;
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
    }
}
