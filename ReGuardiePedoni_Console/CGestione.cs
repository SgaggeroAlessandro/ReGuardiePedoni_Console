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

        public event EventHandler Attacco;
        public event EventHandler<RispostaEventArgs> Messaggio;

        protected virtual void OnAttacco(EventArgs e)
        {
            if (Attacco != null)
            {
                Attacco.Invoke(this, e);
            }
        }

        protected virtual void OnMessaggio(RispostaEventArgs e)
        {
            if (Messaggio != null)
            {
                Messaggio.Invoke(this, e);
            }
        }
        private void Personaggio_Risposta(object sender, RispostaEventArgs e)
        {
            OnMessaggio(e);
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
            for(int i = 3; i< righe.Length && lista.Count < 100; i++)
            {
                string r = righe[i].Trim();
                lista.Add(r);
                if(r == "End")
                {
                    break;
                }
            }
            Comandi = lista.ToArray();
        }

        public void EseguiProssimo()
        {
            if(Finito == true || Prossimo >= Comandi.Length)
            {
                return;
            }
            Esegui(Comandi[Prossimo]);
            Prossimo++;
        }

        public void Esegui(string comando)
        {
            OnMessaggio(new RispostaEventArgs("> " + comando));

            string[] parti = comando.Trim().Split(' ');

            if (parti.Length == 2 && parti[0] == "Attacca" && parti[1] == "Re")
            {
                OnAttacco(EventArgs.Empty);
            }else if(parti.Length == 2 && parti[0] == "Cattura")
            {
                CGuardia g = null;
                foreach (CGuardia x in Guardie)
                {
                    if (x.Nome == parti[1])
                    {
                        g = x;
                        break;
                    }
                }

                CPedone p = null;
                foreach (CPedone y in Pedoni)
                {
                    if (y.Nome == parti[1])
                    {
                        p = y;
                        break;
                    }
                }

                if(g != null)
                {
                    Disiscrivi(g);
                    Guardie.Remove(g);

                }else if(p != null)
                {
                    Disiscrivi(p);
                    Pedoni.Remove(p);
                }
            }else if(parti.Length == 1 && parti[0] == "End")
            {
                OnMessaggio(new RispostaEventArgs("Simulazione terminata"));
                Finito = true;
            }
            else
            {
                OnMessaggio(new RispostaEventArgs("Comando non valido"));
            }
        }

        private void Iscrivi(CPersonaggio p)
        {
            Attacco += p.Gestione_Attacco;
            p.Risposta += Personaggio_Risposta;
        }

        private void Disiscrivi(CPersonaggio p)
        {
            Attacco -= p.Gestione_Attacco;
            p.Risposta -= Personaggio_Risposta;
        }

        private void Pulisci()
        {
            if(Re != null)
            {
                Disiscrivi(Re);
                Re = null;
            }
            while(Guardie.Count > 0)
            {
                Disiscrivi(Guardie[0]);
                Guardie.RemoveAt(0);
            }
            while(Pedoni.Count > 0)
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
