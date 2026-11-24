using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    public class CGuardia : CPersonaggio
    {
        public CGuardia(string nome) : base(nome) { }

        public override void Gestione_Attacco(object sender, EventArgs e)
        {
            OnRisposta(new RispostaEventArgs($"La Guardia {Nome} sta difendendo"));
        }
    }
}
