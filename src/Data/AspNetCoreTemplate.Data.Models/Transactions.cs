namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Transactions : BaseModel<int>
    {
        public DateTime Date { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public Currency TransactionCurrency { get; set; }

        // Postupleniq/Income
        [Required]
        public decimal Receipt { get; set; }

        [Required]
        public decimal Payment { get; set; }

        public DebitCard DebitCard { get; set; }
    }
}
