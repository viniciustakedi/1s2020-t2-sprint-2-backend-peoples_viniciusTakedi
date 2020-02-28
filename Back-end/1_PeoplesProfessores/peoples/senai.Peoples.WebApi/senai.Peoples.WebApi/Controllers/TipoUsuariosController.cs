using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.Peoples.WebApi.Domains;
using senai.Peoples.WebApi.Interfaces;
using senai.Peoples.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]

    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        private ITipoUsuarioRepository _TipoUsuarioRepository { get; set; }

        public TipoUsuariosController()
        {
            _TipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IEnumerable<TipoUsuarioDomain> Get()
        {
            // Faz a chamada para o método .Listar();
            return _TipoUsuarioRepository.Listar();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
           TipoUsuarioDomain TipoUsuarioBuscado = _TipoUsuarioRepository.BuscarPorId(id);

            if (TipoUsuarioBuscado != null)
            {
                return Ok(TipoUsuarioBuscado);
            }
            return NotFound("Não foi encontrado o tipo de usuario buscado.");
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, TipoUsuarioDomain TipoUsuarioAtualizado)
        {
            TipoUsuarioDomain TipoUsuarioBuscado = _TipoUsuarioRepository.BuscarPorId(id);

            if (TipoUsuarioBuscado != null)
            {
                try
                {
                    _TipoUsuarioRepository.Atualizar(id, TipoUsuarioAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }
            return NotFound
                (
                    new
                    {
                        mensagem = "Tipo Usuario não foi encontrado",
                        erro = true
                    }
                );
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TipoUsuarioDomain TipoUsuarioBuscado = _TipoUsuarioRepository.BuscarPorId(id);
            if (TipoUsuarioBuscado != null)
            {
                _TipoUsuarioRepository.Deletar(id);

                return Ok($"Tipo Usuario {id} foi deletado!");
            }
            return NotFound("Tipo Usuario não pode ser encontrado conforme o dado passado.");
        }

    }
}
