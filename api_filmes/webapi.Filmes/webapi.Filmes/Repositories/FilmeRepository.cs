using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;

namespace webapi.Filmes.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {

        // String de conexão com o banco de dados que recebe os seguintes parâmetros:
        //Data Source: Nome do servidor
        //Initial Catalog: Nome do banco de dados
        //Autenticação:
        //             -Windows : Integrated Security = true
        //             -SqlServer : User Id = sa; Pwd = Senha
        private string StringConexao = "Data Source = NOTE02-S15; Initial Catalog = Filmes; User Id = sa; Pwd = Senai@134";

        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {

                string queryIdCorpo = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryIdCorpo, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);

                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    cmd.Parameters.AddWithValue("@IdFilme", filme.IdFilme); 

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarUrl(int id, FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectUrl = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme ";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectUrl, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);

                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Método que busca objeto por id
        /// </summary>
        /// <param name="id"> id do objeto que será buscado</param>
        /// <returns> retorna o objeto buscado ou error caso não seja encontrado</returns>
        public FilmeDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectId = "SELECT Filme.IdGenero, Genero.Nome, IdFilme, Titulo FROM Filme JOIN Genero ON Filme.IdGenero = Genero.IdGenero WHERE IdFilme = @IdFilme ";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectId, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filmeBuscado = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            Titulo = Convert.ToString(rdr["Titulo"]),

                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                                Nome = Convert.ToString(rdr["Nome"])
                            }
                        
                        };
                        return filmeBuscado;
                    }

                    return null;
                }


            }
        }

        /// <summary>
        /// Método de cadastro de objetos(Filme)
        /// </summary>
        /// <param name="novoFilme"> objeto que será cadastrado</param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Filme(Titulo, IdGenero) VALUES (@Titulo, @IdGenero)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", novoFilme.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero", novoFilme.IdGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @IdFIlme";

                con.Open();
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Método que lista todos objetos (filmes)
        /// </summary>
        /// <returns> Lista de objetos</returns>
        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> ListaFilmes = new List<FilmeDomain>();

            //Criando string de conexão do sql
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Consulta que será utilizada para listar objetos no Sql
                string querySelectAll = "SELECT Filme.IdGenero, Genero.Nome, IdFilme, Titulo FROM Filme JOIN Genero ON Filme.IdGenero = Genero.IdGenero";

                //abrindo string de conexão
                con.Open();

                //Criando leitor que irá armamzenar as informações da consulta
                SqlDataReader rdr;

                //passando a query a ser executada e a string de conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {   
                    //armazenando as informações obtida pelo comando 
                    rdr = cmd.ExecuteReader();

                    //validação de repetição para armazenar os objetos na lista
                    while (rdr.Read())
                    {
                        //Criando um objeto para guardar as informações do rdr
                        FilmeDomain filme = new FilmeDomain()
                        {
                            //atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            //atribui a propriedade IdFilme o valor recebido no rdr
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            //atribui a propriedade Nome o valor recebido no rdr
                            Titulo = Convert.ToString(rdr["Titulo"]),

                            //Nome = Convert.ToString(rdr["Nome"])

                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                Nome = Convert.ToString(rdr["Nome"])
                            }
                        };
                        //Adiciona cada objeto dentro da lista
                        ListaFilmes.Add(filme);
                    }
                }

                //retornando a lista com os objetos
                return ListaFilmes;
            }
        }
    }
}
