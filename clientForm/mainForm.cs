using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientForm
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainButton_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Service1Client myService = new ServiceReference1.Service1Client();
                MessageBox.Show(myService.GetData(123), "my service");
                myService.Close();
            }
            catch (Exception ex)   // may be a timeout
            {
                MessageBox.Show(ex.Message, "Error !");
            }
        }
    }
}
