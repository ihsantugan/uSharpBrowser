using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uSharpBrowser;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sharpBrowser1.Navigate("https://uyg.sgk.gov.tr/vizite/welcome.do");
        }

        private void btnGetElement_Click(object sender, EventArgs e)
        {
            JqueryObject obj = sharpBrowser1.GetElementWithJquery(".headerRow");

            obj.NextAll();

        }
    }
}
