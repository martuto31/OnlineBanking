namespace AspNetCoreTemplate.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using AutoMapper;

    public class TransactionsViewModel : IMapFrom<Account>, IMapFrom<DebitCard>, IHaveCustomMappings
    {
        public DebitCard DebitCard { get; set; }

        public IEnumerable<Transactions> Transactions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<DebitCard, DebitCard>();
            configuration.CreateMap<Transactions, Transactions>();
        }
    }
}
