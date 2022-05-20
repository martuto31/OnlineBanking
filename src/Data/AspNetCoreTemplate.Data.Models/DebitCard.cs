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
        [RegularExpression("([0-9]+)", ErrorMessage = "Моля въведете валидна стойност")]
        [StringLength(16, ErrorMessage = "Номерът на картата трябва да се състои от 16 цифри без място")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Моля въведете датата на изтичане на валидността")]
        [Display(Name = "Дата на валидност")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Моля въведете секретният код")]
        [Display(Name = "CVC")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Моля въведете валидна стойност")]
        [StringLength(3)]
        public string CVCCode { get; set; }

        [Display(Name = "Валута")]
        [DisplayFormat(DataFormatString = "{0:Y}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Required]
        public Currency Currency { get; set; }

        [Required]
        public double CardBalance { get; set; }

        public string IBAN { get; set; }

        public Account Account { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
