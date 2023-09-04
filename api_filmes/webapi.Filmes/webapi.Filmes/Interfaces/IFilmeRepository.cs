using webapi.Filmes.Domains;

namespace webapi.Filmes.Interfaces
{
    public interface IFilmeRepository
    {
        //tipoRetorno NomeMetodo(TipoParametro NomeParametro)

        /// <summary>
        /// Serve para cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto sera cadastrado</param>
        void Cadastrar(FilmeDomain novoFilme);

        /// <summary>
        /// Listar todos os objetos cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<FilmeDomain> ListarTodos();

        /// <summary>
        /// Atualizar objeto existente passando seu Id pelo corpo da requisição
        /// </summary>
        /// <param name="filme">Objeto atualizado(novas informações)</param>
        void AtualizarIdCorpo(FilmeDomain filme);

        /// <summary>
        /// Atualizar objeto existente passando seu id pela URL
        /// </summary>
        /// <param name="id">Id do objeto que será atualizado</param>
        /// <param name="filme">Objeto atualizado(novas informações)</param>
        void AtualizarUrl(int id,FilmeDomain filme);

        /// <summary>
        /// Deletar um objeto
        /// </summary>
        /// <param name="id">Id do objeto que sera deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um objeto através de seu id
        /// </summary>
        /// <param name="id"> Id do objeto a ser buscado</param>
        /// <returns>Objeto buscado</returns>
        FilmeDomain BuscarPorId(int id);
    }
}
