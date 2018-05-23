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
        private Corrector corrector;
        
        public UserControl1()
        {
            InitializeComponent();
            corrector = new Corrector();
        }

        private void btnCorregir_Click(object sender, EventArgs e)
        {
            List<String> oracion = new List<String>();
            //divide la oracion en palabras            
            oracion = txtOriginal.Text.ToLower().Split(' ', '\t').ToList();
            var copiaOracion = txtOriginal.Text;
            String palabra = "";
            for (int i = 0; i < oracion.Count(); i++)
            {
                palabra = oracion[i];

                if (!corrector.PalabraExiste(palabra))
                {                
                    // Buscar las palabras a distancia 1
                    List<String> palabrasE1 = corrector.PalabrasDistancia1(palabra);

                    // Para cada palabra buscar la frecuencia de los bigramas
                    Dictionary<String, double> frecuenciasPalabrasE1 = new Dictionary<string, double>();
                    foreach (var palabraE1 in palabrasE1)
                    {
                        var palabraAnterior = i == 0 ? "<s>" : oracion[i - 1];
                        var palabraPosterior = i == oracion.Count() - 1 ? "</s>" : oracion[i + 1];
                        var frecuenciaAnterior = corrector.BuscarFrecuenciaDelBigrama(palabraE1, palabraAnterior);
                        var frecuenciaPosterior = corrector.BuscarFrecuenciaDelBigrama(palabraPosterior, palabraE1);

                        frecuenciasPalabrasE1.Add(palabraAnterior + "," + palabraE1, frecuenciaAnterior);
                        frecuenciasPalabrasE1.Add(palabraE1 + "," + palabraPosterior, frecuenciaPosterior);
                    }
                }
            }            
        }        
    }
}
