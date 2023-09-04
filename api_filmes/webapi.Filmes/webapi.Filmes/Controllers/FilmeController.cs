using Microsoft.AspNetCore.Mvc;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;
using webapi.Filmes.Repositories;

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

    //Método controlador que herda da controlle base
    //Onde será criado Endpoints(rotas)
    public class FilmeController : Controller
    {
        private IFilmeRepository _filmeRepository { get; set; }

        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// EndPoint que aciona o método listarTodos
        /// </summary>
        /// <returns>Uma lista de objetos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<FilmeDomain> ListaFilmes = _filmeRepository.ListarTodos();

                return Ok(ListaFilmes);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }


        /// <summary>
        /// EndPoint que aciona o método de cadastro
        /// </summary>
        /// <param name="novoFilme"> Objeto que será cadastrado</param>
        /// <returns> Status Code 201 (Created)</returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);
                return StatusCode(201);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
        
        /// <summary>
        /// EndPoint que aciona o método de delete
        /// </summary>
        /// <param name="id"> Id do objeto que será deletado</param>
        /// <returns>204 ou BadRequest</returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _filmeRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// EndPoint que aciona o método que busca o objeto pelo id
        /// </summary>
        /// <param name="id"> id do objeto que será buscado</param>
        /// <returns> retorna objeto buscado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

                if(filmeBuscado == null)
                {
                    return NotFound("Nenhum filme foi encontrado!");
                }

                return Ok(filmeBuscado);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// EndPoint que aciona o método de atualizar pela url
        /// </summary>
        /// <param name="id">id do objeto que será atualizado</param>
        /// <param name="filme">objeto (filme) que será atualizado</param>
        /// <returns> retorna novo objeto (filme) atualizado</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, FilmeDomain filme)
        {
            try
            {
                _filmeRepository.AtualizarUrl(id, filme);

                return StatusCode(200);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// EndPoint que aciona o método atualizar pelo corpo
        /// </summary>
        /// <param name="filme"> objeto (filme) que será atualizado</param>
        /// <returns>Status Code (200)</returns>
        [HttpPut]
        public IActionResult Put(FilmeDomain filme)
        {
            try
            {
                _filmeRepository.AtualizarIdCorpo(filme);

                return StatusCode(200);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}
