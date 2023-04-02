using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required (ErrorMessage = "O campo 'Nome' é obrigatório")]
        [StringLength (50, MinimumLength = 3, ErrorMessage ="Este campo deve ter no mínimo 3 e no máximo 50 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo 'Slug' é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo deve ter no mínimo 3 e no máximo 20 caracteres.")]
        public string Slug { get; set; }

    }
}
