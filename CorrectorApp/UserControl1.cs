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
            String oracionConErrores = LimpiarOracion(txtOriginal.Text.ToLower());
            String oracionCorregida = corrector.ObtenerOracionCorregida(oracionConErrores);

            txtCorregido.Text = oracionCorregida;          
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern System.IntPtr CreateRoundRectRgn
          (
           int nLeftRect, // x-coordinate of upper-left corner
           int nTopRect, // y-coordinate of upper-left corner
           int nRightRect, // x-coordinate of lower-right corner
           int nBottomRect, // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
          );

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(System.IntPtr hObject);

        private void btnCorregir_Paint(object sender, PaintEventArgs e)
        {
            System.IntPtr ptr = CreateRoundRectRgn(0, 0, btnCorregir.Width, btnCorregir.Height, 15, 15); // _BoarderRaduis can be adjusted to your needs, try 15 to start.
            btnCorregir.Region = System.Drawing.Region.FromHrgn(ptr);
            DeleteObject(ptr);
        }

        private String LimpiarOracion(String oracion)
        {
            return oracion
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace("-", "")
                    .Replace("_", "")
                    .Replace("@", "")
                    .Replace("¡", "")
                    .Replace("!", "")
                    .Replace("¿", "")
                    .Replace("?", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("=", "")
                    .Replace("+", "")
                    .Replace("%", "")
                    .Replace("$", "")
                    .Replace("~", "")
                    .Replace("'", "")
                    .Replace("*", "")
                    .Replace(@"\", "")
                    .Replace("/", "")
                    .Replace("& quot;", "")
                    .Replace("&quot;", "")
                    .Replace("&quot", "")
                    .Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "")
                    .Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "").Replace("0", "");
        }
    }
}
