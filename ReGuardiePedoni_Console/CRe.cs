using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGuardiePedoni_Console
{
    public class CRe : CPersonaggio
    {
        public CRe(string nome) : base(nome) { }

        public override void Gestione_Attacco(object sender, EventArgs e)
        {
            string testo = $"Il Re {Nome} è sotto attacco";
            File.AppendAllText("output.txt", testo + "\n");
            OnRisposta(new RispostaEventArgs(testo));
        }
    }
}
