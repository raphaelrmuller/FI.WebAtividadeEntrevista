using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }

        /// <summary>
        /// Inclui ou altera uma lista de beneficiarios
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long IncluirAlterarExcluir(List<DML.Beneficiario> beneficiarios,long idCliente)
        {
            DAL.DaoBeneficiario daoBeneficiario = new DAL.DaoBeneficiario();            
            return daoBeneficiario.IncluirAlterarDeletar(beneficiarios,idCliente);            
        }

        /// <summary>
        /// Lista os beneficiarios de um determinado cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        public List<DML.Beneficiario> Listar(long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Listar(idCliente);
        }

        /// <summary>
        /// Metodo que verifica se o CPF já foi cadastrado para o cliente
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarExistencia(CPF, idCliente);
        }

        /// <summary>
        /// metodo que verifica se os CPFs já foram cadastrados para o cliente
        /// </summary>
        /// <param name="CPFs"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public List<string> VerificarExistencia(List<string> CPFs, long idCliente)
        {
            List<string> lista = new List<string>();
            DAL.DaoBeneficiario daoB = new DAL.DaoBeneficiario();
            foreach (var cpf in CPFs)
            {
                if (daoB.VerificarExistencia(cpf, idCliente))
                    lista.Add("CPF: " + cpf + " já cadastrado para este cliente.");

            }
            return lista;
        }
    }
}
