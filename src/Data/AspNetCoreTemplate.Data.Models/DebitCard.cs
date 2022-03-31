namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AspNetCoreTemplate.Data.Common.Models;

    public class DebitCard : BaseModel<int>
    {
        [Required(ErrorMessage = "Моля въведете номерът на картата")]
        [Display(Name = "Номер на картата")]
        [StringLength(16)]
        public int CardNumber { get; set; }

        [Required(ErrorMessage = "Моля въведете датата на изтичане на валидността")]
        [Display(Name = "Дата на валидност")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3)]
        [Required(ErrorMessage = "Моля въведете секретният код")]
        [Display(Name = "CVC")]
        public int CVCCode { get; set; }

        [Display(Name = "Валута")]
        [DisplayFormat(DataFormatString = "{0:Y}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]

        [Required]
        public Currency Currency { get; set; }

        public double CardBalance
        {
            get { return this.CardBalance; }
            set { this.CardBalance = 0.00f; }
        }

        public Account Account { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
