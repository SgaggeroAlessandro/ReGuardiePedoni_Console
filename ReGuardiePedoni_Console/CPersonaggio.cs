using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    public class RispostaEventArgs : EventArgs
    {
        public string Testo { get; }
        public RispostaEventArgs(string testo)
        {
            Testo = testo;
        }
    }


    public abstract class CPersonaggio
    {
        public string Nome { get; }
        public CPersonaggio(string Nome)
        {
            this.Nome = Nome;
        }

        public event EventHandler<RispostaEventArgs> Risposta;
        protected virtual void OnRisposta(RispostaEventArgs e)
        {
            if(Risposta != null)
            {
                Risposta.Invoke(this, e);
            }
        }

        public abstract void Gestione_Attacco(object sender, EventArgs e);
    }
}
