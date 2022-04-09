namespace AspNetCoreTemplate.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using AutoMapper;

    public class DebitCardViewModel : IMapFrom<Account>, IMapFrom<DebitCard>, IHaveCustomMappings
    {
        public Account Account { get; set; }

        public DebitCard DebitCard { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Account, Account>();
            configuration.CreateMap<DebitCard, DebitCard>();
        }
    }
}
