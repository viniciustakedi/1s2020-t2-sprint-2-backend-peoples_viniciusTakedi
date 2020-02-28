using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.People.WebApi.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}
