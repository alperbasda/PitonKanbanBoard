using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Dtos
{
    public class UserForRegisterDto : IDto
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Lütfen Mail Adresinizi Girin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Girin")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen Adınızı Girin")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen Soyadınızı Girin")]
        public string LastName { get; set; }
    }
}
