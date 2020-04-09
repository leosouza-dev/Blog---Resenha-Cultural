using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo Titulo é Obrigatório")]
        [StringLength(50, ErrorMessage = "Digite no máximo 50 caracteres")]
        [Display(Name = "Título: ")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Campo Resumo é obrigatório")]
        [Display(Name = "Resumo: ")]
        public string Resumo { get; set; }

        [Display(Name = "Categoria: ")]
        public string Categoria { get; set; }

        public DateTime? DataPublicacao { get; set; }
        public bool Publicado { get; set; }
    }
}
