using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Models.ValueObjects
{

    public class CPF
    {
        public CPF()
        {

        }
        
        public CPF(string cpf)
        {
            _numeroCPF = ClearFormat(cpf);

        }
        
        private string _numeroCPF;
        
        
        public string NumeroCPF
        {
            get => _numeroCPF;
            set
            {
                _numeroCPF = ClearFormat(value);
            }
        }
        
        private string ClearFormat(string cpf)
        {
            return Regex.Replace(cpf, "[^0-9]", "");
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CPF other = (CPF)obj;
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

        public static implicit operator CPF(string numero)
        {
            return new CPF(numero);
        }

        public static implicit operator string(CPF cpf)
        {
            return cpf.NumeroCPF;
        }
    }

}