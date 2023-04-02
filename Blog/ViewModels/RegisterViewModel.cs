using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O Nome é OBRIGATÓRIO")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O Email é OBRIGATÓRIO")]
        [EmailAddress(ErrorMessage = "O Email é INVÁLIDO")]
        public string Email { get; set; }
        //public string PassWord { get; set; }
    }
}
