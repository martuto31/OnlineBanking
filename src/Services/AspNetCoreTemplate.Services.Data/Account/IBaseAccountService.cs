namespace AspNetCoreTemplate.Services.Data.Account
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AspNetCoreTemplate.Data.Models;

    public interface IBaseAccountService
    {
        Account GetAccount(string username);

        bool CheckIfAccountExist(string username);
    }
}
