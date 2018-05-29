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
            var oracionConErrores = txtOriginal.Text.ToLower();
            var oracionCorregida = corrector.ObtenerOracionCorregida(oracionConErrores);

            txtCorregido.Text = oracionCorregida;          
        }        
    }
}
