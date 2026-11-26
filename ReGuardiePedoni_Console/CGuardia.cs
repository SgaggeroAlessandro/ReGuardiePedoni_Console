using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Comunica($"La guardia{Nome} ");

namespace ReGuardiePedoni_Console
{
    public class CGuardia : CPersonaggio
    {
        public CGuardia(string nome) : base(nome) { }

        public override void OnAttaccoAlRe(object sender, EventArgs e)
        {
            Comunica($"La Guardia {Nome} sta difendendo");
        }
    }
}
