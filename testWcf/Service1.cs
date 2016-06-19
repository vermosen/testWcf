using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;

namespace testWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        protected Mutex dictMutex_;
        protected Dictionary<Guid, iTestCallback> clients_;

        Service1()
        {
            clients_ = new Dictionary<Guid, iTestCallback>();
            dictMutex_ = new Mutex();
        }

        public void register(Guid id)
        {
            lock (dictMutex_)
            {
                // operation context: current client calling the service ?
                clients_.Add(id, OperationContext.Current.GetCallbackChannel<iTestCallback>());
            }
        }
        public void unregister(Guid id)
        {
            lock (dictMutex_)
            {
                // operation context: current client calling the service ?
                clients_.Remove(id);
            }
        }
        public void beat(DateTime time, Guid sender, Guid target)
        {
            lock (dictMutex_)
            {
                if (target == Guid.Empty)
                {
                    // broadcast to all
                    foreach (var i in clients_)
                    {
                        if (i.Key != sender)
                        {
                            i.Value.beatCallback(time, sender, target);
                        }
                    }
                }
                else
                {
                    // try to beat only 1 guy
                    if (clients_.ContainsKey(target))
                    {
                        clients_[target].beatCallback(time, sender, target);
                    }
                }
                
                // operation context: current client calling the service ?
            }
        }
    }
}
