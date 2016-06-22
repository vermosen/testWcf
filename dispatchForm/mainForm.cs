using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;

namespace dispatchForm
{
    public partial class mainForm : Form, ServiceReference1.IService1Callback
    {
        private InstanceContext inst_;
        private ServiceReference1.Service1Client wcfClient_;
        private Guid id_;
        private Mutex textBoxMutex_;
        private Mutex dictionaryMutex_;
        private Dictionary<Guid, DateTime> status_;

        public mainForm()
        {
            id_ = Guid.NewGuid();
            textBoxMutex_ = new Mutex();
            dictionaryMutex_ = new Mutex();
            inst_ = new InstanceContext(this);
            status_ = new Dictionary<Guid, DateTime>();
            wcfClient_ = new ServiceReference1.Service1Client(inst_);
            wcfClient_.register(id_, true);
            requestTermination_ = new ManualResetEvent(false);
            terminated_ = new ManualResetEvent(false);

            InitializeComponent();
        }
        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopBeat();
            wcfClient_.unregister(id_);
            wcfClient_.Close();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            checkConnectionButton.PerformClick();
        }
        private void checkConnectionButton_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            if (b.Text.ToUpper() == "STOP")
            {
                stopBeat();
                b.Text = "Start";
            }
            else if (b.Text.ToUpper() == "START")
            {
                startBeat();
                b.Text = "Stop";
                // TODO: set the flags requestTermination_ and terminated_ back 
            }
            else
            {
                // message box error
            }
        }
    }
}
