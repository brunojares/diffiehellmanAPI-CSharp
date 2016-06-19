using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicativo
{
    public class Aplicativo
    {
        public static void Main(string[] args)
        {
            using (var _emissor = new Emissor())
            using (var _receptor = new Receptor())
            {
                var _mensagem = _emissor.envia(_receptor, "Teste 123");
                Console.WriteLine(_receptor.recebe(_emissor, _mensagem));
                Console.ReadKey();
            }
        }
    }
}
