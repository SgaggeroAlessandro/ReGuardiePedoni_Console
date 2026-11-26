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
        public event EventHandler Catturato;
        protected void Comunica(string testo)
        {
            if (Risposta != null)
            {
                Risposta.Invoke(this, new RispostaEventArgs(testo));
            }
        }

        public abstract void OnAttaccoRe(object sender, EventArgs e);
        public virtual void OnCattura(object sender, string nome)
        {
            if (nome != Nome)
            {
                return;
            }
            if (Catturato != null)
            {
                Catturato.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
