namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Currency
    {
        [Display(Name = "Евро")]
        EUR = 1,
        [Display(Name = "Щатски долар")]
        USD = 2,
        [Display(Name = "Лева")]
        BGN = 3,
        [Display(Name = "Рубли")]
        RUB = 4,
        [Display(Name = "Британска лира")]
        GBP = 5,
    }
}
