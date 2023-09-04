using System.Data.SqlClient;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;

namespace webapi.Filmes.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // String de conexão com o banco de dados que recebe os seguintes parâmetros:
        //Data Source: Nome do servidor
        //Initial Catalog: Nome do banco de dados
        //Autenticação:
        //             -Windows : Integrated Security = true
        //             -SqlServer : User Id = sa; Pwd = Senha
        private string StringConexao = "Data Source = NOTE02-S15; Initial Catalog = Filmes; User Id = sa; Pwd = Senai@134";

        public UsuarioDomain Login(string Email, string Senha)
        {  

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryLogin = "SELECT IdUsuario, Senha, Email, Permissao FROM Usuario WHERE Email = @Email AND Senha = @Senha ";

                SqlDataReader rdr;

                con.Open();
                using (SqlCommand cmd = new SqlCommand(queryLogin, con))
                {
                     
                      
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Senha", Senha);
        
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain user = new UsuarioDomain()
                        {
                            Email = rdr["Email"].ToString(),

                            Permissao = Convert.ToBoolean(rdr["Permissao"]),

                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"])

                        };

                        return user;
                    }
                    return null;
                }
            }
        }
    }
}
