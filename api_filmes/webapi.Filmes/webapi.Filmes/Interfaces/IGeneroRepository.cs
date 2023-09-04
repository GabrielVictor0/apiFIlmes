using webapi.Filmes.Domains;

namespace webapi.Filmes.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório GeneroRepository
    /// Definir os métodos que serão implementados pelo GeneroRepository
    /// </summary>
    public interface IGeneroRepository
    {
        //tipoRetorno NomeMetodo(TipoParametro NomeParametro)
        /// <summary>
        /// Serve para cadastrar um novo genero
        /// </summary>
        /// <param name="novoGenero">Objeto sera cadastrado</param>
        void Cadastrar(GeneroDomain novoGenero);

        /// <summary>
        /// Listar todos os objetos cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<GeneroDomain> ListarTodos();

        /// <summary>
        /// Atualizar objeto existente passando seu Id pelo corpo da requisição
        /// </summary>
        /// <param name="genero">Objeto atualizado(novas informações)</param>
        void AtualizarIdCorpo(GeneroDomain genero);

        /// <summary>
        /// Atualizar objeto existente passando seu id pela URL
        /// </summary>
        /// <param name="id">Id do objeto que será atualizado</param>
        /// <param name="genero">Objeto atualizado(novas informações)</param>
        void AtualizarUrl(int id, GeneroDomain genero);

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
        GeneroDomain BuscarPorId(int id);
    }
}
