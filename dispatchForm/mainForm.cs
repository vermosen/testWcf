using System;
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

        public mainForm()
        {
            id_ = Guid.NewGuid();
            textBoxMutex_ = new Mutex();
            inst_ = new InstanceContext(this);
            wcfClient_ = new ServiceReference1.Service1Client(inst_);
            wcfClient_.register(id_, true);
            InitializeComponent();
        }
        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            wcfClient_.unregister(id_);
            wcfClient_.Close();
        }
        private void checkConnectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                // send a general call
                lock (textBoxMutex_)
                {
                    mainTextBox.AppendText("connection check..." + Environment.NewLine);
                }
                wcfClient_.beat(DateTime.UtcNow, id_, Guid.Empty);
            }
            catch (Exception ex)                        // may be a timeout here
            {
                lock (textBoxMutex_)
                {
                    mainTextBox.AppendText("An error has been raised: " 
                        + ex.Message + Environment.NewLine);
                }
            }
        }
        void ServiceReference1.IService1Callback.beatCallback(DateTime time, Guid sender, Guid target)
        {
            if (target == Guid.Empty)
            {
                // call from a client, returns the call
                lock (textBoxMutex_)
                {
                    mainTextBox.AppendText("get ping from " + sender + Environment.NewLine);
                }
                
                wcfClient_.beat(time, id_, sender);
            }
            else
            {
                lock (textBoxMutex_)
                {
                    mainTextBox.AppendText(sender + " replied in "
                    + (DateTime.UtcNow - time).Milliseconds
                    + " ms" + Environment.NewLine);
                }
            }
        }
    }
}
