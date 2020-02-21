using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.webAPI.Interfaces;
using Senai.Peoples.webAPI.Repositories;
using Senai.Peoples.webAPI.Domains;

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
       

    }
}
