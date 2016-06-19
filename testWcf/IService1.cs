using System;
using System.ServiceModel;

namespace testWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(iTestCallback))]
    public interface IService1
    {
        [OperationContract(IsOneWay = true)] void register(Guid id);
        [OperationContract(IsOneWay = true)] void unregister(Guid id);
        [OperationContract(IsOneWay = true)] void beat(DateTime time, Guid sender, Guid target);
    }

    public interface iTestCallback
    {
        [OperationContract(IsOneWay = true)] void beatCallback(DateTime time, Guid sender, Guid target);
    }
}
