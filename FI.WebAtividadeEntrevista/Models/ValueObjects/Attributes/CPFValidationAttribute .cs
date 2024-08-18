using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FI.WebAtividadeEntrevista.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Models.Attributes
{

    public class CPFValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cpf = value as CPF;

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return new ValidationResult("O CPF não pode ser vazio ou nulo.");
            }

            var cleancpf = ClearFormat(cpf);

            if (!VerifyIsValid(cleancpf))
            {
                return new ValidationResult($"O CPF informado é inválido.{cpf}");
            }

            return ValidationResult.Success;
        }

        private string ClearFormat(string cpf)
        {
            return Regex.Replace(cpf, "[^0-9]", "");
        }

        public static bool VerifyIsValid(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (cpf == new string(char.Parse(j.ToString()), 11))
                    return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }

}