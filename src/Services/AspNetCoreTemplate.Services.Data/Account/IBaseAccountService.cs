namespace AspNetCoreTemplate.Services.Data.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AspNetCoreTemplate.Data.Models;

    public interface IBaseAccountService
    {
        Account GetAccount(string username);

        bool CheckIfAccountExist(string username);

        bool CheckIfDebitCardExist(DebitCard debitCard);

        IEnumerable<TViewModel> GetAccDebitCards<TViewModel>(Account account);

        IEnumerable<TViewModel> GetAllTransactions<TViewModel>(DebitCard debitCard);

        DebitCard GetDebitCard(int id);

        void AddFunds(DebitCard debitCard, int receiverDebitCardId, int amountOfFunds);

    }
}
