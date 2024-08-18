using FI.WebAtividadeEntrevista.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
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
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// CPF do Beneficiário
        /// </summary>
        private CPFObjectValue _cpf;
        [Required]
        public string CPF
        {
            get { return _cpf == null ? "" : _cpf.ToString(); }
            set { _cpf = new CPFObjectValue(value); }
        }
    }
}