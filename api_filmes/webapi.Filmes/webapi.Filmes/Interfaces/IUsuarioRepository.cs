using webapi.Filmes.Domains;

namespace webapi.Filmes.Interfaces
{
    public interface IUsuarioRepository
    {
        UsuarioDomain Login(string Email, string Senha);
    }
}
