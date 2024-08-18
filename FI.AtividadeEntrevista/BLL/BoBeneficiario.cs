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
        public long IncluirAlterar(List<DML.Beneficiario> beneficiarios)
        {
            DAL.DaoBeneficiario daoBeneficiario = new DAL.DaoBeneficiario();
            int retorno = 0;
            foreach (var beneficiario in beneficiarios)
            {                
                if (beneficiario.Id > 0)
                {
                    daoBeneficiario.Alterar(beneficiario);
                }else
                {
                    daoBeneficiario.Incluir(beneficiario);
                }
                retorno++;
            }
            return retorno;
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

        public bool VerificarExistencia(string CPF, long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarExistencia(CPF, idCliente);
        }

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
