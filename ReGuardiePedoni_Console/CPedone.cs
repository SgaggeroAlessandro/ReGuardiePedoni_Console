using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Comunica($"Il pedone {Nome} si sta preparando");
namespace ReGuardiePedoni_Console
{
    public class CPedone : CPersonaggio
    {
        public CPedone(string nome) : base(nome) { }

        public override void OnAttaccoRe(object sender, EventArgs e)
        {
            Comunica($"Il Pedone {Nome} si sta preparando");
        }
    }
}
