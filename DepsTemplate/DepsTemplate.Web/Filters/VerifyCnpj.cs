using System.Linq;

namespace DepsTemplate.Web.Filters
{
    public class VerifyCnpj
    {
        public static bool IsValid(string cnpj)
        {
            if (cnpj.Length != 14)
            {
                return false;
            }

            if (cnpj.Distinct().Count() == 1)
            {
                return false;
            }

            //Primeiro digito
            int soma = 0;
            int multiplicador = 5;
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicador;
                if (multiplicador == 2)
                {
                    multiplicador = 9;
                }
                else
                {
                    multiplicador = multiplicador - 1;
                }
            }
            int primeiroDigito;
            if (soma % 11 < 2)
            {
                primeiroDigito = 0;
            }
            else
            {
                primeiroDigito = 11 - (soma % 11);
            }

            if (int.Parse(cnpj[12].ToString()) != primeiroDigito)
            {
                return false;
            }

            //Segundo digito
            soma = 0;
            multiplicador = 6;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicador;
                if (multiplicador == 2)
                {
                    multiplicador = 9;
                }
                else
                {
                    multiplicador = multiplicador - 1;
                }
            }
            int segundoDigito;
            if (soma % 11 < 2)
            {
                segundoDigito = 0;
            }
            else
            {
                segundoDigito = 11 - (soma % 11);
            }

            if (int.Parse(cnpj[13].ToString()) != segundoDigito)
            {
                return false;
            }

            //cnpj valido
            return true;
        }
    }
}
