using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    public abstract class CPersonaggio
    {
        public string Nome { get; }

        public CPersonaggio(string nomePersonaggio)
        {
            Nome = nomePersonaggio;
        }

        public event EventHandler<string> Messaggio;

        protected void Comunica(string frase)
        {
            if (Messaggio != null)
            {
                Messaggio.Invoke(this, frase);
            }
        }

        public abstract void OnAttaccoAlRe(object sender, EventArgs e);

        public virtual void OnCattura(object sender, string nomeCatturato)
        {
            if (nomeCatturato != Nome)
            {
                return;
            }
            if (sender is CGestione mittenteGestione)
            {
                mittenteGestione.AttaccoAlRe -= OnAttaccoAlRe;
                mittenteGestione.Cattura -= OnCattura;
            }
        }
    }
}