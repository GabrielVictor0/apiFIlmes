using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Domains
{
    /// <summary>
    /// Ela é uma classe que representa a entidade(tabela) Genero
    /// </summary>
    public class GeneroDomain
    {
        public int IdGenero { get; set; }

        [Required(ErrorMessage ="O nome do gênero é obrigatório!")]
        public string? Nome { get; set; }
    }
}
