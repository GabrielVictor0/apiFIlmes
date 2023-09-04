using System.Data.SqlClient;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;

namespace webapi.Filmes.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        // String de conexão com o banco de dados que recebe os seguintes parâmetros:
        //Data Source: Nome do servidor
        //Initial Catalog: Nome do banco de dados
        //Autenticação:
        //             -Windows : Integrated Security = true
        //             -SqlServer : User Id = sa; Pwd = Senha
        private string StringConexao = "Data Source = NOTE02-S15; Initial Catalog = Filmes; User Id = sa; Pwd = Senai@134";
        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateCorpo = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdateCorpo, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarUrl(int id, GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdate = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    cmd.ExecuteNonQuery();
                }

            }
        }


        /// <summary>
        /// Buscar um gênero através de um id
        /// </summary>
        /// <param name="id">Id do genero a ser buscado</param>
        /// <returns> Objeto buscado ou null caso não seja encontrado</returns>
        public GeneroDomain BuscarPorId(int id)
        {   
            
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectById = "SELECT IdGenero, Nome FROM Genero WHERE IdGenero = @IdGenero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader rdr para receber os valores do banco de dados
                SqlDataReader rdr;
                
                //Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {   
                    //Passa o valor para o parâmetro @IdGenero
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    //Executa a query e armazena os dados no rdr 
                    rdr = cmd.ExecuteReader();

                    //Verifica se o resultado da query retornou algum registro
                    if (rdr.Read())
                    {
                        //se sim, instancia um novo objeto generoBuscado do tipo GeneroDomain
                        GeneroDomain generoBuscado = new GeneroDomain
                        {   
                            //Atribui à propriedade IdGenero o valor da coluna "IdGenero" da tabela do banco de dados
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            //Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco de dados
                            Nome = rdr["Nome"].ToString()
                        };
                        //retorna o generoBuscado com os dados obtidos
                        return generoBuscado;
                    }

                    //Se não, retorna null
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastrar um novo genero
        /// </summary>
        /// <param name="novoGenero">Objeto Com as informacoes que serao cadastradas</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            //Declara a string de conexaso como parametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que sera executada
                string queryInsert = "INSERT INTO Genero(Nome)  VALUES (@Nome)";

                //Abre a conexao com o banco de dados 
                con.Open();


                //Declara o SqlCommand passando a query que sera executada e a conexao do banco 
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();

                }


            }
        }

        public void Deletar(int id)
        {
            //Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Genero WHERE IdGenero= @IdDelete";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdDelete",id );

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos (gêneros)
        /// </summary>
        /// <returns>Lista de objetos (gêneros)</returns>
        public List<GeneroDomain> ListarTodos()
        {   
            //Cria uma lista de objetos do tipo Gênero
            List<GeneroDomain> ListaGeneros = new List<GeneroDomain>();

            //Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query que será executada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {   
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        //Criando um objeto para guardar as informações do rdr
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[0]),

                            //atribui a propriedade Nome o valor recebido no rdr
                            Nome = Convert.ToString(rdr["Nome"])
                        };
                        //Adiciona cada objeto dentro da lista
                        ListaGeneros.Add(genero);
                    }
                }
            }
            //Retorna a lista de gêneros
            return ListaGeneros;
        }
    }
}
