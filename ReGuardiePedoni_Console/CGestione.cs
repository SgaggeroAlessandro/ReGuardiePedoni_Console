using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//int maxcomandi = 100
//no liste per guardie e pedoni
//string nomere get private set
//array di nomi guardi e array di nomi pedoni
//array di righe
//int prossima riga

//public bool cisonocomandi --> nel get se terminato return false se
//public event eventhandler attaccoalre
//public event eventhandler<string>? cattura
//carica file --> se le righe sono <3 errore, prossima riga parte da 3
//eseguiprossimo --> se comando è attacco al re --> attaccoalre?.invoke(this,eventargs.empty)
//se comando inizia con cattura

//tutta la logica principale è gestita nel gestore, volendo string nomere get private set array di nomi guardi e array di nomi pedoni

//per avalonia --> 3 "zone", quello di sinistra per i personaggi, quello a destra per il registro e quello sotto per i bottoni --> meglio usare il dockpanel al posto dello stackpanel
//dock panel per mettere gli elementi
//wrap panel --> per i tre personaggi, così si adatta in automatico
//observablecollection --> come una lista, ma genera un evento solo quando aggiungo o tolgo un elemento, chi sta ascoltando se ne accorge e reagisce


namespace ReGuardiePedoni_Console
{
    public class CGestione
    {
        public const int LimiteComandi = 100;

        public string NomeRe { get; private set; } = "";
        public string[] NomiGuardie { get; private set; } = new string[0];
        public string[] NomiPedoni { get; private set; } = new string[0];

        private string[] contenutoFile = new string[0];
        private int indiceComando;
        private int contatoreComandi;
        private bool simulazioneFinita;

        public event EventHandler AttaccoAlRe;
        public event EventHandler<string> Cattura;

        public bool CiSonoComandi
        {
            get
            {
                if (simulazioneFinita == true)
                {
                    return false;
                }
                if (contatoreComandi == LimiteComandi)
                {
                    return false;
                }
                return indiceComando < contenutoFile.Length;
            }
        }

        public string ProssimoComando
        {
            get
            {
                return contenutoFile[indiceComando].Trim();
            }
        }

        public void CaricaFile(string percorsoFile)
        {
            contenutoFile = File.ReadAllLines(percorsoFile);
            if (contenutoFile.Length < 3)
            {
                throw new InvalidDataException("Il file deve contenere almeno le tre righe con i nomi.");
            }

            NomeRe = contenutoFile[0].Trim();
            NomiGuardie = DividiNomi(contenutoFile[1]);
            NomiPedoni = DividiNomi(contenutoFile[2]);

            indiceComando = 3;
            contatoreComandi = 0;
            simulazioneFinita = false;
            SaltaRigheVuote();
        }

        public bool EseguiProssimo()
        {
            if (CiSonoComandi == false)
            {
                throw new InvalidOperationException("Non ci sono altri comandi da eseguire.");
            }

            string testoComando = ProssimoComando;
            indiceComando++;
            contatoreComandi++;
            SaltaRigheVuote();

            if (testoComando == "Attacca Re")
            {
                if (AttaccoAlRe != null)
                {
                    AttaccoAlRe.Invoke(this, EventArgs.Empty);
                }
                return true;
            }

            if (testoComando.StartsWith("Cattura "))
            {
                string nomeBersaglio = testoComando.Substring(8).Trim();
                if (Cattura != null)
                {
                    Cattura.Invoke(this, nomeBersaglio);
                }
                return true;
            }

            if (testoComando == "End")
            {
                simulazioneFinita = true;
                return true;
            }

            return false;
        }

        private string[] DividiNomi(string linea)
        {
            List<string> elenco = new List<string>();
            string[] pezzi = linea.Split(' ');
            foreach (string pezzo in pezzi)
            {
                if (pezzo != "")
                {
                    elenco.Add(pezzo);
                }
            }
            return elenco.ToArray();
        }

        private void SaltaRigheVuote()
        {
            while (indiceComando < contenutoFile.Length && contenutoFile[indiceComando].Trim() == "")
            {
                indiceComando++;
            }
        }
    }
    }

