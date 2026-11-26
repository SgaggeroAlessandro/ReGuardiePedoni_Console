using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//event handler in ognguna delle tre classi
//classe astratta personaggio, non serve catturato perché basta rimuovere
//si può fare una classe gestione generale, che deve leggere tutto il file, strutturare un array di stringhe, ad ogni click va avanti di un comando, è lui che chiama e generare gli eventi
//re, guardia, pedone sono ascoltatori (destinatari) hanno il gestore dell'evento, il mittente (chi genera l'evento), logica INDIPENDENTE dal view (MVC)
//classe gestionale dichiara i due eventi, legge il file, struttura un array di stringhe (ogni stringa è una riga), generare l'evento, con apri viene fatto tutto questo, tranne la generazione dell'evento
//la logica deve essere fatta in modo tale che se voglio fare lo stesso esercizio in Console, le classi non cambiano minimamente
// prima in console per vedere la gestione delle due classi (solo per capire la logica che ci sta sotto, deve essere fatto in modo tale che domani lo copio e incollo su avalonia, aggiungo la view e deve funzionare lo stesso), non servono molti controlli
/*
 virtual void OnCattura if(nome != Nome) return 
 */

namespace ReGuardiePedoni_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CGestione gestione = new CGestione();
            gestione.Messaggio += Gestione_Messaggio;

            string percorso = "input.txt";
            if (File.Exists(percorso) == false)
            {
                Console.WriteLine("File input.txt non trovato in: " + Directory.GetCurrentDirectory());
                return;
            }

            gestione.Carica(percorso);

            Console.WriteLine("Premi invio per ogni passo");
            while (gestione.Finito == false && gestione.Prossimo < gestione.Comandi.Length)
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
