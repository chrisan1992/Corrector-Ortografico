using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CorrectorApp
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static UserControl1 controlCorregir = new UserControl1();


        public Form1()
        {
            InitializeComponent();
            //sets the first screen to the user
            this.btnCorrector.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnConfig.ForeColor = System.Drawing.Color.White;
            this.panelPrincipal.Controls.Clear();
            this.panelPrincipal.Controls.Add(controlCorregir);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.IntPtr ptr = CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15); // _BoarderRaduis can be adjusted to your needs, try 15 to start.
            this.Region = System.Drawing.Region.FromHrgn(ptr);
            DeleteObject(ptr);

        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(System.IntPtr hObject);

        private void labelClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void labelMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// To be able to move te form without border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnCorrector_Click(object sender, EventArgs e)
        {
            this.btnCorrector.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnConfig.ForeColor = System.Drawing.Color.White;
            this.panelPrincipal.Controls.Clear();
            this.panelPrincipal.Controls.Add(controlCorregir);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            this.btnCorrector.ForeColor = System.Drawing.Color.White;
            this.btnConfig.ForeColor = System.Drawing.Color.DodgerBlue;
            this.panelPrincipal.Controls.Clear();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
