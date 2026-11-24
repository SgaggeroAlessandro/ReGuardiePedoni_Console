using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CGestione gestione = new CGestione();
            gestione.Messaggio += Gestione_Messaggio;

            Console.Write("File di input: ");
            gestione.Carica(Console.ReadLine());

            Console.WriteLine("Premi invio per ogni passo");
            while(gestione.Finito == false && gestione.Prossimo < gestione.Comandi.Length)
            {
                Console.ReadLine();
                gestione.EseguiProssimo();
            }
        }
        static void Gestione_Messaggio(object sender, RispostaEventArgs e)
        {
            Console.WriteLine(e.Testo);
        }

    }
}
