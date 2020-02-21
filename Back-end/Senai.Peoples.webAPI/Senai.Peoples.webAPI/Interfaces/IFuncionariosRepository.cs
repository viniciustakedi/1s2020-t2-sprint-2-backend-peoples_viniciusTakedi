using Senai.Peoples.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.webAPI.Interfaces
{
    interface IFuncionariosRepository
    {

        //Para listar todos os funcionarios
        List <FuncionarioDomain> Listar();                      //Lista todos os funcionarios

        void Inserir(FuncionarioDomain funcionario);            //Para inserir novos funcionarios

        void AtualizarPorUrl(int id, FuncionarioDomain funcionario);     //Atualiza um funcionario passando somente o ID pelo link

        void Deletar(int id);                                   //Deleta o funcionario passando o Id

        void BuscarPorId(int id);                                //Busca funcionario pelo id

    }
}
