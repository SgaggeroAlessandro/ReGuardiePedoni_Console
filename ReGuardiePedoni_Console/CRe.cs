using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Comunica($"Il re {Nome} ");
//Comunica scatena l'evento
//gli eventi possono essere scatenati solo nella classe che gli dichiara, nemmeno nelle sottoclassi
//override void OnCattura vuoto perché il re non può essere catturato
namespace ReGuardiePedoni_Console
{
    public class CRe : CPersonaggio
    {
        public CRe(string nome) : base(nome) { }

        public override void OnAttaccoAlRe(object sender, EventArgs e)
        {
            base.Comunica($"Il re {Nome} è sotto attacco");
        }

        
        public override void OnCattura(object sender, string nome)
        {
        }
    }
}
