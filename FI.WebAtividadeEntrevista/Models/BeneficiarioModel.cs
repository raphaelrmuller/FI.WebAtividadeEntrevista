using FI.WebAtividadeEntrevista.Models.Attributes;
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
        /// <summary>
        /// CPF
        /// </summary>

        [Required]
        [CPFValidationAttribute]
        public CPF CPF { get; set; }
    }
}