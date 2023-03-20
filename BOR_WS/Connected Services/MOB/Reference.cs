﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BOR_WS.MOB {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string responseCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string responseMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VERIFICATION_RESULTField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string commentsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string responseCode {
            get {
                return this.responseCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.responseCodeField, value) != true)) {
                    this.responseCodeField = value;
                    this.RaisePropertyChanged("responseCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string responseMessage {
            get {
                return this.responseMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.responseMessageField, value) != true)) {
                    this.responseMessageField = value;
                    this.RaisePropertyChanged("responseMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string VERIFICATION_RESULT {
            get {
                return this.VERIFICATION_RESULTField;
            }
            set {
                if ((object.ReferenceEquals(this.VERIFICATION_RESULTField, value) != true)) {
                    this.VERIFICATION_RESULTField = value;
                    this.RaisePropertyChanged("VERIFICATION_RESULT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string comments {
            get {
                return this.commentsField;
            }
            set {
                if ((object.ReferenceEquals(this.commentsField, value) != true)) {
                    this.commentsField = value;
                    this.RaisePropertyChanged("comments");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MOB.MobSoap")]
    public interface MobSoap {
        
        // CODEGEN: Generating message contract since element name mob from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MobCheck", ReplyAction="*")]
        BOR_WS.MOB.MobCheckResponse MobCheck(BOR_WS.MOB.MobCheckRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MobCheck", ReplyAction="*")]
        System.Threading.Tasks.Task<BOR_WS.MOB.MobCheckResponse> MobCheckAsync(BOR_WS.MOB.MobCheckRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class MobCheckRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="MobCheck", Namespace="http://tempuri.org/", Order=0)]
        public BOR_WS.MOB.MobCheckRequestBody Body;
        
        public MobCheckRequest() {
        }
        
        public MobCheckRequest(BOR_WS.MOB.MobCheckRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class MobCheckRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string mob;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string id;
        
        public MobCheckRequestBody() {
        }
        
        public MobCheckRequestBody(string mob, string id) {
            this.mob = mob;
            this.id = id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class MobCheckResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="MobCheckResponse", Namespace="http://tempuri.org/", Order=0)]
        public BOR_WS.MOB.MobCheckResponseBody Body;
        
        public MobCheckResponse() {
        }
        
        public MobCheckResponse(BOR_WS.MOB.MobCheckResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class MobCheckResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public BOR_WS.MOB.Result MobCheckResult;
        
        public MobCheckResponseBody() {
        }
        
        public MobCheckResponseBody(BOR_WS.MOB.Result MobCheckResult) {
            this.MobCheckResult = MobCheckResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MobSoapChannel : BOR_WS.MOB.MobSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MobSoapClient : System.ServiceModel.ClientBase<BOR_WS.MOB.MobSoap>, BOR_WS.MOB.MobSoap {
        
        public MobSoapClient() {
        }
        
        public MobSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MobSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MobSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MobSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BOR_WS.MOB.MobCheckResponse BOR_WS.MOB.MobSoap.MobCheck(BOR_WS.MOB.MobCheckRequest request) {
            return base.Channel.MobCheck(request);
        }
        
        public BOR_WS.MOB.Result MobCheck(string mob, string id) {
            BOR_WS.MOB.MobCheckRequest inValue = new BOR_WS.MOB.MobCheckRequest();
            inValue.Body = new BOR_WS.MOB.MobCheckRequestBody();
            inValue.Body.mob = mob;
            inValue.Body.id = id;
            BOR_WS.MOB.MobCheckResponse retVal = ((BOR_WS.MOB.MobSoap)(this)).MobCheck(inValue);
            return retVal.Body.MobCheckResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<BOR_WS.MOB.MobCheckResponse> BOR_WS.MOB.MobSoap.MobCheckAsync(BOR_WS.MOB.MobCheckRequest request) {
            return base.Channel.MobCheckAsync(request);
        }
        
        public System.Threading.Tasks.Task<BOR_WS.MOB.MobCheckResponse> MobCheckAsync(string mob, string id) {
            BOR_WS.MOB.MobCheckRequest inValue = new BOR_WS.MOB.MobCheckRequest();
            inValue.Body = new BOR_WS.MOB.MobCheckRequestBody();
            inValue.Body.mob = mob;
            inValue.Body.id = id;
            return ((BOR_WS.MOB.MobSoap)(this)).MobCheckAsync(inValue);
        }
    }
}
