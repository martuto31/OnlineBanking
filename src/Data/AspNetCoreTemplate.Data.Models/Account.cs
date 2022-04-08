namespace AspNetCoreTemplate.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Account : BaseModel<int>
    {
        [Required(ErrorMessage = "Моля въведете потребителско име")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Моля попълнете празното място")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля попълнете празното място")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Моля въведете имейл адрес")]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Моля въведете имейл адрес")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Моля въведете имейл адрес")]
        [Display(Name = "Възраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Моля въведете парола")]
        [Display(Name = "Парола")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Моля повторете парола")]
        [Display(Name = "Потвърдете паролата")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public ICollection<DebitCard> DebitCards { get; set; }

        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Моля изберете валута")]
        [Display(Name = "Изберете валута")]
        public Currency Currency { get; set; }

        [Required]
        public double AccountBalance { get; set; }

        public string IBAN { get; set; }
    }
}
