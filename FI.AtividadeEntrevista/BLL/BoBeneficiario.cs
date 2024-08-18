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
        public long Incluir(List<DML.Beneficiario> beneficiarios)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Incluir(beneficiarios);
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

        public bool VerificarExistencia(string CPF,long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarExistencia(CPF,idCliente);
        }
    }
}
