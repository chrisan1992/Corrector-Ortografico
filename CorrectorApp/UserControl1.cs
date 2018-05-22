using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CorrectorApp
{
    public partial class UserControl1 : UserControl
    {
        public static Corrector corrector;
        
        public UserControl1()
        {
            InitializeComponent();
            corrector = new Corrector();
        }

        private void btnCorregir_Click(object sender, EventArgs e)
        {
            List<String> oracion = new List<String>();
            //divide la oracion en palabras
            oracion = txtOriginal.Text.Split(' ', '\t').ToList();
            foreach (String palabra in oracion)
            {
                if (corrector.PalabraExiste(palabra))
                {
                    //palabra si se encuentra en el diccionario
                }
                else
                {
                    List<String> palabrasE1 = PalabraDistancia1(palabra);
                }
            }
            
        }

        
    }
}
