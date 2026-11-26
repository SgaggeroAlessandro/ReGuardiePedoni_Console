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

//una lista di personaggio, nessuna lista in gestionale
//metodo arruola

//re.Messaggio += Personaggio_Messaggio -->quando re invoca messaggio il main rispondi eseguendo Personaggio_Messaggio

//personaggio, gestionale, main
//personaggio ha re,guardia,pedone
//solo il main può scrivere
//gestore legge il file
//attaccaRe, Cattura(nome) sono due eventi dichiarati nel gestionale
//CHI DICHIARA E' CHI SCATENA
//AD AttaccaRe rispondono tutte e tre le classi, quindi si può dichiarare in Personaggio
//a cattura deve rispondere o la guardia o il pedone
//re quando è sotto attacco deve mandare un messaggio, e anche guardia e pedone --> evento che viene passato al main
//nel main ci sarà += che assegnerà il gestore all'evento nel personaggio (Messaggio)
//gestore lancia l'evento che fa partire quello che c'è nella classe personaggio, e lo richiama nel main
//due event handler nel personaggio o in una classe derivata e 1 nel main
//quindi 3 eventi in totale, due da gestore file e uno lanciato da personaggio
//DICHIARO LANCIO E CATTURO
//+= serve per mettere in ascolto

//try-catch(per il file di input)
//arruolo tutti i personaggi 
//while(gestore.cisonocomandi)

//arruola(persona p) --> p.messaggio += stampaMessaggio; gestore.attacoalre += p.reonattaccoalre; gestore.cattura += p.oncattura
//stampamessaggio(objecte? sender, string frase) --> console.writeline(frase)
namespace ReGuardiePedoni_Console
{
    internal class Program
    {
        private const string nomeFileInput = "input.txt";
        private const string nomeFileOutput = "output.txt";

        private static CGestione gestioneComandi = new CGestione();

        static void Main(string[] args)
        {
            try
            {
                gestioneComandi.CaricaFile(nomeFileInput);
            }
            catch (Exception errore)
            {
                Console.WriteLine("Impossibile leggere il file di input: " + errore.Message);
                return;
            }

            File.WriteAllText(nomeFileOutput, "");

            Arruola(new CRe(gestioneComandi.NomeRe));

            foreach (string nomeGuardia in gestioneComandi.NomiGuardie)
            {
                Arruola(new CGuardia(nomeGuardia));
            }

            foreach (string nomePedone in gestioneComandi.NomiPedoni)
            {
                Arruola(new CPedone(nomePedone));
            }

            while (gestioneComandi.CiSonoComandi)
            {
                Console.WriteLine("> " + gestioneComandi.ProssimoComando);

                if (gestioneComandi.EseguiProssimo() == false)
                {
                    Console.WriteLine("Comando non valido");
                }
            }

            Console.WriteLine("Simulazione terminata");
        }

        static void Arruola(CPersonaggio personaggio)
        {
            personaggio.Messaggio += StampaMessaggio;
            gestioneComandi.AttaccoAlRe += personaggio.OnAttaccoAlRe;
            gestioneComandi.Cattura += personaggio.OnCattura;
        }

        static void StampaMessaggio(object sender, string frase)
        {
            Console.WriteLine(frase);

            CRe reMittente = sender as CRe;
            if (reMittente != null)
            {
                File.AppendAllText(nomeFileOutput, frase + "\n");
            }
        }
    }
}
