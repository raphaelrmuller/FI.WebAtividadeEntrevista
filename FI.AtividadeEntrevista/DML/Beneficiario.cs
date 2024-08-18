using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        /// <summary>
        /// Id do Beneficiário
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id do Cliente
        /// </summary>
        public long IdCliente { get; set; }

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>

        public string Nome { get; set; }

        /// <summary>
        /// CPF do Beneficiário
        /// </summary>
        public string CPF { get; set; }
    }
}
