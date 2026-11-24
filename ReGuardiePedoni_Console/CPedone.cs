using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    public class CPedone : CPersonaggio
    {
        public CPedone(string nome) : base(nome) { }

        public override void Gestione_Attacco(object sender, EventArgs e)
        {
            OnRisposta(new RispostaEventArgs($"Il Pedone {Nome} si sta preparando"));
        }
    }
}
