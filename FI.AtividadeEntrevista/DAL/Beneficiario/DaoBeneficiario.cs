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
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));            
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));
            base.Executar("FI_SP_AltBenef", parametros);
        } 
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

        internal List<DML.Beneficiario> Listar(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));
            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            return Converter(ds);
        }


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

        internal bool VerificarExistencia(string CPF, long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("@CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarios", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

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

        internal long IncluirAlterarDeletar(List<Beneficiario> beneficiarios)
        {
            int retorno = 0;
            var beneficiariosExistentes = Listar(beneficiarios.First().IdCliente);
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

        internal void Excluir(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", id));
            base.Executar("FI_SP_DelBenef", parametros);
        }

        
    }
}
