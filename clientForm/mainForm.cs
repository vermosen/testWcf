﻿using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace clientForm
{
    public partial class mainForm : Form, ServiceReference1.IService1Callback
    {
        private InstanceContext inst_;
        private ServiceReference1.Service1Client wcfClient_;
        private Guid id_;

        public mainForm()
        {
            InitializeComponent();
            id_ = Guid.NewGuid();
            inst_ = new InstanceContext(this);
            wcfClient_ = new ServiceReference1.Service1Client(inst_);
            wcfClient_.register(id_);
        }
        ~mainForm()
        {
            wcfClient_.unregister(id_);
            wcfClient_.Close();
        }
        private void mainButton_Click(object sender, EventArgs e)
        {
            try
            {
                wcfClient_.beat(DateTime.UtcNow, id_, Guid.Empty);
            }
            catch (Exception ex)                        // may be a timeout here
            {
                MessageBox.Show(ex.Message, "Error !");
            }
        }

        void ServiceReference1.IService1Callback.beatCallback(DateTime time, Guid sender, Guid target)
        {
            if (target == Guid.Empty)
            {
                // was a general call, answering
                wcfClient_.beat(time, id_, sender);
            }
            else
            {
                MessageBox.Show("get a reply from " + sender + " in " 
                    + (DateTime.UtcNow - time).Milliseconds 
                    + " ms", "Information");
            }
        }
    }
}
