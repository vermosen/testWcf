﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dispatchForm.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1", CallbackContract=typeof(dispatchForm.ServiceReference1.IService1Callback))]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/register")]
        void register(System.Guid id, bool dispatcher);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/register")]
        System.Threading.Tasks.Task registerAsync(System.Guid id, bool dispatcher);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/unregister")]
        void unregister(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/unregister")]
        System.Threading.Tasks.Task unregisterAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/beat")]
        void beat(System.DateTime time, System.Guid sender, System.Guid target);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/beat")]
        System.Threading.Tasks.Task beatAsync(System.DateTime time, System.Guid sender, System.Guid target);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Callback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService1/beatCallback")]
        void beatCallback(System.DateTime time, System.Guid sender, System.Guid target);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : dispatchForm.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.DuplexClientBase<dispatchForm.ServiceReference1.IService1>, dispatchForm.ServiceReference1.IService1 {
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void register(System.Guid id, bool dispatcher) {
            base.Channel.register(id, dispatcher);
        }
        
        public System.Threading.Tasks.Task registerAsync(System.Guid id, bool dispatcher) {
            return base.Channel.registerAsync(id, dispatcher);
        }
        
        public void unregister(System.Guid id) {
            base.Channel.unregister(id);
        }
        
        public System.Threading.Tasks.Task unregisterAsync(System.Guid id) {
            return base.Channel.unregisterAsync(id);
        }
        
        public void beat(System.DateTime time, System.Guid sender, System.Guid target) {
            base.Channel.beat(time, sender, target);
        }
        
        public System.Threading.Tasks.Task beatAsync(System.DateTime time, System.Guid sender, System.Guid target) {
            return base.Channel.beatAsync(time, sender, target);
        }
    }
}
