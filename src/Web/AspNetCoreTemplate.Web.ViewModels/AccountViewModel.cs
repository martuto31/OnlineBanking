namespace AspNetCoreTemplate.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using AutoMapper;

    public class AccountViewModel : IMapFrom<Account>, IMapFrom<DebitCard>, IHaveCustomMappings
    {
        public Account Account { get; set; }

        public IEnumerable<DebitCard> DebitCards { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Account, Account>();
            configuration.CreateMap<DebitCard, DebitCard>();
        }
    }
}
