using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Domains
{
    public class FilmeDomain 
    {
        public int IdFilme { get; set; }

        public int IdGenero { get; set; }
        [Required (ErrorMessage ="O título do filme é obrigatório!")]
        public string? Titulo { get; set; }

        //Referência da classe genero
        public GeneroDomain? Genero { get; set; }
    }
}
