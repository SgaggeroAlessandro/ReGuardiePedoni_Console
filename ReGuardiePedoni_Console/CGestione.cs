using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    public class CGestione
    {
        public CRe Re { get; private set; }
        public ObservableCollection<CGuardia> Guardie { get; } = new ObservableCollection<CGuardia>();
        public ObservableCollection<CPedone> Pedoni { get; } = new ObservableCollection<CPedone>();

        public string[] Comandi { get; private set; } = new string[0];
        public int Prossimo { get; private set; }
        public bool Finito { get; private set; }

        public event EventHandler AttaccoRe;

        public event EventHandler<string> Cattura;
        public event EventHandler<RispostaEventArgs> Messaggio;

        protected void Comunica(string testo)
        {
            if (Messaggio != null)
            {
                Messaggio.Invoke(this, new RispostaEventArgs(testo));
            }
        }

        private void Personaggio_Risposta(object sender, RispostaEventArgs e)
        {
            Comunica(e.Testo);
        }


        private void Personaggio_Catturato(object sender, EventArgs e)
        {
            CGuardia g = sender as CGuardia;
            CPedone p = sender as CPedone;

            if (g != null)
            {
                Disiscrivi(g);
                Guardie.Remove(g);
            }
            else if (p != null)
            {
                Disiscrivi(p);
                Pedoni.Remove(p);
            }
        }

        public void Carica(string percorso)
        {
            string[] righe = File.ReadAllLines(percorso);

            Pulisci();

            File.WriteAllText("output.txt", "");

            Re = new CRe(righe[0].Trim());
            Iscrivi(Re);

            string[] nomi = righe[1].Split(' ');
            foreach (string s in nomi)
            {
                if (s != "")
                {
                    CGuardia g = new CGuardia(s);
                    Iscrivi(g);
                    Guardie.Add(g);
                }
            }

            string[] nomiPedoni = righe[2].Split(' ');
            foreach (string s in nomiPedoni)
            {
                if (s != "")
                {
                    CPedone p = new CPedone(s);
                    Iscrivi(p);
                    Pedoni.Add(p);
                }
            }

            List<string> lista = new List<string>();
            for (int i = 3; i < righe.Length && lista.Count < 100; i++)
            {
                string r = righe[i].Trim();
                lista.Add(r);
                if (r == "End")
                {
                    break;
                }
            }
            Comandi = lista.ToArray();
        }

        public void EseguiProssimo()
        {
            if (Finito == true || Prossimo >= Comandi.Length)
            {
                return;
            }
            Esegui(Comandi[Prossimo]);
            Prossimo++;
        }

        public bool Esegui(string comando)
        {
            comando = comando.Trim();
            Comunica("> " + comando);

            if (comando == "Attacca Re")
            {
                if (AttaccoRe != null)
                {
                    AttaccoRe.Invoke(this, EventArgs.Empty);
                }
                return true;
            }

            if (comando.StartsWith("Cattura "))
            {
                // "Cattura " sono 8 caratteri: il nome inizia dalla posizione 8
                string nome = comando.Substring(8).Trim();
                if (Cattura != null)
                {
                    Cattura.Invoke(this, nome);
                }
                return true;
            }

            if (comando == "End")
            {
                Comunica("Simulazione terminata");
                Finito = true;
                return true;
            }

            Comunica("Comando non valido");
            return false;
        }

        private void Iscrivi(CPersonaggio p)
        {
            AttaccoRe += p.OnAttaccoRe;
            Cattura += p.OnCattura;
            p.Risposta += Personaggio_Risposta;
            p.Catturato += Personaggio_Catturato;
        }

        private void Disiscrivi(CPersonaggio p)
        {
            AttaccoRe -= p.OnAttaccoRe;
            Cattura -= p.OnCattura;
            p.Risposta -= Personaggio_Risposta;
            p.Catturato -= Personaggio_Catturato;
        }

        private void Pulisci()
        {
            if (Re != null)
            {
                Disiscrivi(Re);
                Re = null;
            }
            while (Guardie.Count > 0)
            {
                Disiscrivi(Guardie[0]);
                Guardie.RemoveAt(0);
            }
            while (Pedoni.Count > 0)
            {
                Disiscrivi(Pedoni[0]);
                Pedoni.RemoveAt(0);
            }
            Comandi = new string[0];
            Prossimo = 0;
            Finito = false;
        }

    }
}