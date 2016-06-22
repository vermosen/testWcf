using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dispatchForm
{
    public partial class mainForm
    {
        // for heartbeat interruption, local to heartbeat thread
        ManualResetEvent requestTermination_;
        ManualResetEvent terminated_;

        void ServiceReference1.IService1Callback.beatCallback(DateTime time, Guid sender, Guid target)
        {
            if (target == Guid.Empty)
            {
                // beat initiated by the client, returns the call
                lock (textBoxMutex_)
                {
                    mainTextBox.AppendText("get ping from " + sender + Environment.NewLine);
                }

                wcfClient_.beat(time, id_, sender);
            }
            else
            {
                // reply from heartbeat
                lock (textBoxMutex_)
                {
                    mainTextBox.AppendText(sender + " replied in "
                    + (DateTime.UtcNow - time).Milliseconds
                    + " ms" + Environment.NewLine);
                }
                lock (dictionaryMutex_)
                {
                    if (status_.ContainsKey(sender))
                    {
                        status_[sender] = DateTime.UtcNow;
                    }
                    else
                    {
                        status_.Add(sender, DateTime.UtcNow);
                    }
                }
            }
        }

        // check for the connectivity every x seconds + interruption loop
        private void beatLoop()
        {
            Stopwatch chrono = new Stopwatch();
            chrono.Start();

            while (!requestTermination_.WaitOne(0))
            {
                // beat every 10 secs
                if (chrono.Elapsed > TimeSpan.FromMilliseconds(10000))
                {
                    wcfClient_.beat(DateTime.UtcNow, id_, Guid.Empty);
                    chrono.Restart();
                }
            }

            terminated_.Set();
        }
        private void startBeat()
        {
            // we need to make sure there is only 1 active thread
            requestTermination_.Reset();
            terminated_.Reset();

            // start a new thread
            Thread t = new Thread(new ThreadStart(this.beatLoop));
            t.Start();
        }
        private void stopBeat()
        {
            // interrupt the current thread
            requestTermination_.Set();
            terminated_.WaitOne();
        }
    }
}
