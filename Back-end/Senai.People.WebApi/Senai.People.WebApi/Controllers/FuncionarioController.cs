using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.webAPI.Interfaces;
using Senai.Peoples.webAPI.Repositories;
using Senai.Peoples.webAPI.Domains;
using Microsoft.AspNetCore.Authorization;

namespace Senai.Peoples.webAPI.Controllers
{
       [Produces("application/json")]

       [Route("api/[controller]")]

       [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        public FuncionarioController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            return _funcionarioRepository.Listar();
        }


        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            _funcionarioRepository.Inserir(novoFuncionario);

            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioBuscado == null)
            {
                return NotFound("Não foi encontrado nunhum funcionario");
            }
            return Ok(funcionarioBuscado);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FuncionarioDomain funcionarioAtualizado)
        {
           FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionario não pode ser encontrado",
                        }
                    );
            }

            try
            {
               _funcionarioRepository.AtualizarPorUrl(id, funcionarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _funcionarioRepository.Deletar(id);
            return Ok("Funcionario foi deletado");
        }

        [HttpGet("buscar/{busca}")]
        public IActionResult GetByName(string busca)
        {
            // Faz a chamada para o método .BuscarPorNome()
            // Retorna a lista e um status code 200 - Ok
            return Ok(_funcionarioRepository.BuscarPorNome(busca));
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("nomescompletos")]
        public IActionResult GetFullName()
        {
            // Faz a chamada para o método .ListarNomeCompleto            
            // Retorna a lista e um status code 200 - Ok
            return Ok(_funcionarioRepository.ListarNomeCompleto());
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("ordenacao/{ordem}")]
        public IActionResult GetOrderBy(string ordem)
        {
            // Verifica se a ordenação atende aos requisitos
            if (ordem != "ASC" && ordem != "DESC")
            {
                // Caso não, retorna um status code 404 - BadRequest com uma mensagem de erro
                return BadRequest("Não é possível ordenar da maneira solicitada. Por favor, ordene por 'ASC' ou 'DESC'");
            }

            // Retorna a lista ordenada com um status code 200 - OK
            return Ok(_funcionarioRepository.ListarOrdenado(ordem));
        }
    }
}
