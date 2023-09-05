using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;
using webapi.Filmes.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webapi.Filmes.Controllers
{
    //Define que a rota de uma requisição será no seguinte formato
    //dominio/api/nomeController
    // ex: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Define que é um Controlador de api
    [ApiController]

    //Define que o tipo de resposta da api será no formato JSON
    [Produces("application/json")]

    [Authorize]

    //Método controlador que herda da controlle base
    //Onde será criado Endpoints(rotas)
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// objeto _generoRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _generoRepository para que haja referência aos métodos no repositório
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Endpoint que aciona o método ListarTodos no repositório e retorna a resposta para o usuário(front-end)
        /// </summary>
        /// <returns>Resposta para o usuário(front-end)</returns>
        [HttpGet]
        [Authorize (Roles = "True, False")]
        public IActionResult Get()
        {
            try
            {
                //cria uma lista que recebe os dados da requisição
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                //retorna a lista no formato JSON com o status code Ok(200)
                return Ok(listaGeneros);
            }
            catch (Exception erro)
            {
                //retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// EndPoind que aciona o metodo de cadastro genero  
        /// </summary>
        /// <param name="novoGenero"> Objeto recebido na requisicao</param>
        /// <returns> Status code 201 (Created)</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                //Chamada para o metodo cadastrar pssando o objeto como um padrao 
                _generoRepository.Cadastrar(novoGenero);

                //Retorna um status code 201 
                return StatusCode(201);
            }
            catch (Exception error)
            {
                //Retorna um status code 400 e a mensagem de erro 
                return BadRequest(error.Message);
            }

        }


        /// <summary>
        /// EndPoint que aciona o método de excluir pelo id
        /// </summary>
        /// <param name="id">Id do genero que será excluido</param>
        /// <returns> Status Code 204 (No Content)(</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }


        /// <summary>
        /// EndPoint que aciona o método BuscarPorId
        /// </summary>
        /// <param name="id">Id do objeto que será buscado</param>
        /// <returns>Se existir o onjeto, retorna o generoBuscado. Se não, retorna null</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

                if (generoBuscado == null)
                {
                    return NotFound("Nenhum genero foi encontrado!");
                }

                return Ok(generoBuscado);
            }
            catch (Exception error)
            {
                //retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(error.Message);

            }

        }

        /// <summary>
        /// EndPoint que aciona o método AtualizarUrl
        /// </summary>
        /// <param name="id"> Id do Genero que será buscado</param>
        /// <param name="Genero">Genero que sera atualizado</param>
        /// <returns>Retorna o novo valor do objeto</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, GeneroDomain Genero)
        {
            try
            {
                _generoRepository.AtualizarUrl(id, Genero);

                return StatusCode(200);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }


        /// <summary>
        /// EndPoint que executa o método AtualizarIdCorpo
        /// </summary>
        /// <param name="Genero">Objeto que será atualizado</param>
        /// <returns>Retorna o novo objeto atualizado</returns>
        [HttpPut]
        public IActionResult Put(GeneroDomain Genero)
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(Genero);

                return StatusCode(200);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message) ;
            }
        }
    }
}
    

