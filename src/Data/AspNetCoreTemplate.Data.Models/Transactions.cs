namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Transactions : BaseModel<int>
    {
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public Currency TransactionCurrency { get; set; }

        // Postupleniq/Income
        public double Receipt { get; set; }

        public double Payment { get; set; }

        public DebitCard DebitCard { get; set; }
    }
}
