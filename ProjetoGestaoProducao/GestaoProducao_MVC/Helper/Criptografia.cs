using System.Security.Cryptography;
using System.Text;

namespace GestaoProducao_MVC.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            //Instancia o gerador
            var hash = SHA1.Create();

            //Instancia o codificador
            var encoding = new ASCIIEncoding();

            //Cria uma lista de bytes com o codificador
            var array = encoding.GetBytes(valor);

            //Gera um hash com o SHA1 enviando p array de bytes
            array = hash.ComputeHash(array);

            //Cria uma string
            var strHexa = new StringBuilder();


            //Roda um laço montando minha string com cada byte do hash;
            foreach(var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }
            //retorna a string para armazenar no banco.
            return strHexa.ToString();
        }







    }
}
