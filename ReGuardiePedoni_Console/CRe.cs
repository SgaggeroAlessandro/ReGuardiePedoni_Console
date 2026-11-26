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

        public override void OnAttaccoRe(object sender, EventArgs e)
        {
            string testo = $"Il Re {Nome} è sotto attacco";
            File.AppendAllText("output.txt", testo + "\n");
            Comunica(testo);
        }

        
        public override void OnCattura(object sender, string nome)
        {
        }
    }
}
