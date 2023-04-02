using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "O Email é OBRIGATÓRIO")]
        [EmailAddress(ErrorMessage = "O Email é INVÁLIDO")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Senha é OBRIGATÓRIA")]
        public string Password { get; set; }





    }
}
