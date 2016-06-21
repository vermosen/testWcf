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
        protected Guid dispatcherId_;
        protected Dictionary<Guid, iTestCallback> clients_;

        Service1()
        {
            dispatcherId_ = Guid.Empty;
            clients_ = new Dictionary<Guid, iTestCallback>();
            dictMutex_ = new Mutex();
        }

        public void register(Guid id, bool dispatcher = false)
        {
            lock (dictMutex_)
            {
                if (!clients_.ContainsKey(id))
                {
                    if (dispatcher == true)
                    {
                        if (dispatcherId_ == Guid.Empty)
                        {
                            dispatcherId_ = id;
                        }
                    }
                    clients_.Add(id, OperationContext.Current.GetCallbackChannel<iTestCallback>());
                }
            }
        }
        public void unregister(Guid id)
        {
            lock (dictMutex_)
            {
                // operation context: current client calling the service ?
                if (id == dispatcherId_) dispatcherId_ = Guid.Empty;
                clients_.Remove(id);
            }
        }
        public void beat(DateTime time, Guid sender, Guid target)
        {
            lock (dictMutex_)
            {
                if (sender == dispatcherId_)
                {
                    if (target == Guid.Empty)
                    {
                        // check all
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
                        clients_[target].beatCallback(time, sender, target);
                    }
                }
                else
                {
                    // if slave + target empty, slave just checked for dispatcher state
                    if (target == Guid.Empty)
                    {
                        clients_[dispatcherId_].beatCallback(time, sender, Guid.Empty);
                    }
                    else
                    {
                        // passthrough
                        clients_[target].beatCallback(time, sender, target);
                    }
                }
            }
        }
    }
}
