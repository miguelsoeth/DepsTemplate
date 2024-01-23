using System.Linq;

namespace DepsTemplate.Web.Filters
{
    public class VerifyCpf
    {
        public static bool IsValid(string cpf)
        {
            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpf.Distinct().Count() == 1)
            {
                return false;
            }

            //Primeiro digito
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            int primeiroDigito = 11 - (soma % 11);
            if (primeiroDigito > 9)
            {
                primeiroDigito = 0;
            }

            if (int.Parse(cpf[9].ToString()) != primeiroDigito)
            {
                return false;
            }

            //Segundo digito
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            int segundoDigito = 11 - (soma % 11);
            if (segundoDigito > 9)
            {
                segundoDigito = 0;
            }

            if (int.Parse(cpf[10].ToString()) != segundoDigito)
            {
                return false;
            }

            //cpf valido
            return true;
        }
    }
}
