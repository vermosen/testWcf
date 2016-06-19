using System.ServiceProcess;
using System.ServiceModel;

namespace hostService
{
    public partial class hostService : ServiceBase
    {
        internal static ServiceHost myServiceHost = null;

        public hostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
            }
            myServiceHost = new ServiceHost(typeof(testWcf.Service1));
            myServiceHost.Open();
        }


        protected override void OnStop()
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
                myServiceHost = null;
            }
        }

    }
}
