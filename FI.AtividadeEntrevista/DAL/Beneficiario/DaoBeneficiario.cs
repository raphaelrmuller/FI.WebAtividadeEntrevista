using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Metodo que altera um beneficiario
        /// </summary>
        /// <param name="beneficiario"></param>
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));
            base.Executar("FI_SP_AltBenef", parametros);
        }
       
        /// <summary>
        /// Metodo que inclui um beneficiario
        /// </summary>
        /// <param name="beneficiario"></param>
        /// <returns></returns>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.IdCliente));
            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Metodo que lista os beneficiarios de um cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        internal List<DML.Beneficiario> Listar(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));
            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            return Converter(ds);
        }

        /// <summary>
        /// Metodo que converte um dataset em uma lista de beneficiarios
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new DML.Beneficiario()
                    {
                        Id = row.Field<long>("Id"),
                        IdCliente = row.Field<long>("IdCliente"),
                        Nome = row.Field<string>("Nome"),
                        CPF = row.Field<string>("CPF")
                    });
                }
                return lista;
            }
            return new List<DML.Beneficiario>();
        }

        /// <summary>
        /// Metodo que verifica a existencia de um beneficiario
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        internal bool VerificarExistencia(string CPF, long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("@CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarios", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Metodo que inclui uma lista de beneficiarios
        /// </summary>
        /// <param name="beneficiarios"></param>
        /// <returns></returns>
        internal long Incluir(List<Beneficiario> beneficiarios)
        {
            int countInclusoes = 0;
            foreach (var beneficiario in beneficiarios)
            {
                Incluir(beneficiario);
                countInclusoes++;
            }
            return countInclusoes;
        }
        
        /// <summary>
        /// Metodo que inclui, altera e deleta beneficiarios
        /// </summary>
        /// <param name="beneficiarios"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        internal long IncluirAlterarDeletar(List<Beneficiario> beneficiarios, long idCliente)
        {
            int retorno = 0;
            var beneficiariosExistentes = Listar(idCliente);
            foreach (var beneficiario in beneficiarios)
            {
                if (beneficiario.Id > 0)
                {
                    Alterar(beneficiario);
                }
                else
                {
                    Incluir(beneficiario);
                }
                retorno++;
            }
            foreach (var beneficiario in beneficiariosExistentes)
            {
                if (!beneficiarios.Any(b => b.Id == beneficiario.Id))
                {
                    Excluir(beneficiario.Id);
                    retorno++;
                }
            }
            return retorno;
        }

        /// <summary>
        /// Metodo que exclui um beneficiario
        /// </summary>
        /// <param name="id"></param>
        internal void Excluir(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", id));
            base.Executar("FI_SP_DelBenef", parametros);
        }


    }
}
