using senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Peoples.WebApi.Interfaces
{
    interface IUsuarioRepository
    {

        List<UsuarioDomain> Listar();
        void Cadastrar(UsuarioDomain novoUsuario);
        UsuarioDomain BuscarPorId(int id);
        void Atualizar(int id, UsuarioDomain UsuarioAtualizado);
        void Deletar(int id);

    }
}
