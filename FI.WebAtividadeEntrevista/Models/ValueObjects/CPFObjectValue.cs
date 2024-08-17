using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Models.ValueObjects
{

    public class CPFObjectValue
    {
        public string NumeroCPF { get; set; }
        public string MessageError { get; }
        public bool IsValid { get; }

        public CPFObjectValue(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                MessageError = "O CPF não pode ser vazio ou nulo.";
            }
            numero = ClearFormat(numero);
            if (!VerifyIsValid(numero))
            {
                MessageError = "O CPF informado é inválido.";
            }
            else
            {
                IsValid = true;
            }
            NumeroCPF = numero;
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CPFObjectValue other = (CPFObjectValue)obj;
            return NumeroCPF == other.NumeroCPF;
        }

        public override int GetHashCode()
        {
            return NumeroCPF.GetHashCode();
        }

        public override string ToString()
        {
            return Convert.ToUInt64(NumeroCPF).ToString(@"000\.000\.000\-00");
        }

        public static implicit operator CPFObjectValue(string numero)
        {
            return new CPFObjectValue(numero);
        }

        public static implicit operator string(CPFObjectValue cpf)
        {
            return cpf.NumeroCPF;
        }
    }

}