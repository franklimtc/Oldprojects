﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonitorTrocasSuprimentosII.wsEmail {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="sEmail", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class sEmail : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailCopiaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailCopiaOcultaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailEnviadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailParaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HtmlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdEmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MensagemField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TituloField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmailCopia {
            get {
                return this.EmailCopiaField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailCopiaField, value) != true)) {
                    this.EmailCopiaField = value;
                    this.RaisePropertyChanged("EmailCopia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmailCopiaOculta {
            get {
                return this.EmailCopiaOcultaField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailCopiaOcultaField, value) != true)) {
                    this.EmailCopiaOcultaField = value;
                    this.RaisePropertyChanged("EmailCopiaOculta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmailEnviado {
            get {
                return this.EmailEnviadoField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailEnviadoField, value) != true)) {
                    this.EmailEnviadoField = value;
                    this.RaisePropertyChanged("EmailEnviado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmailPara {
            get {
                return this.EmailParaField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailParaField, value) != true)) {
                    this.EmailParaField = value;
                    this.RaisePropertyChanged("EmailPara");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Html {
            get {
                return this.HtmlField;
            }
            set {
                if ((this.HtmlField.Equals(value) != true)) {
                    this.HtmlField = value;
                    this.RaisePropertyChanged("Html");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdEmail {
            get {
                return this.IdEmailField;
            }
            set {
                if ((object.ReferenceEquals(this.IdEmailField, value) != true)) {
                    this.IdEmailField = value;
                    this.RaisePropertyChanged("IdEmail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mensagem {
            get {
                return this.MensagemField;
            }
            set {
                if ((object.ReferenceEquals(this.MensagemField, value) != true)) {
                    this.MensagemField = value;
                    this.RaisePropertyChanged("Mensagem");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Titulo {
            get {
                return this.TituloField;
            }
            set {
                if ((object.ReferenceEquals(this.TituloField, value) != true)) {
                    this.TituloField = value;
                    this.RaisePropertyChanged("Titulo");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wsEmail.IsEmail")]
    public interface IsEmail {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsEmail/Enviar", ReplyAction="http://tempuri.org/IsEmail/EnviarResponse")]
        bool Enviar(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsEmail/Enviar", ReplyAction="http://tempuri.org/IsEmail/EnviarResponse")]
        System.Threading.Tasks.Task<bool> EnviarAsync(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsEmail/Listar", ReplyAction="http://tempuri.org/IsEmail/ListarResponse")]
        MonitorTrocasSuprimentosII.wsEmail.sEmail[] Listar();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsEmail/Listar", ReplyAction="http://tempuri.org/IsEmail/ListarResponse")]
        System.Threading.Tasks.Task<MonitorTrocasSuprimentosII.wsEmail.sEmail[]> ListarAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsEmail/EmailUsuario", ReplyAction="http://tempuri.org/IsEmail/EmailUsuarioResponse")]
        string EmailUsuario(string SerieEquipamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsEmail/EmailUsuario", ReplyAction="http://tempuri.org/IsEmail/EmailUsuarioResponse")]
        System.Threading.Tasks.Task<string> EmailUsuarioAsync(string SerieEquipamento);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IsEmailChannel : MonitorTrocasSuprimentosII.wsEmail.IsEmail, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IsEmailClient : System.ServiceModel.ClientBase<MonitorTrocasSuprimentosII.wsEmail.IsEmail>, MonitorTrocasSuprimentosII.wsEmail.IsEmail {
        
        public IsEmailClient() {
        }
        
        public IsEmailClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IsEmailClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IsEmailClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IsEmailClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Enviar(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html) {
            return base.Channel.Enviar(emailPara, emailCopia, emailCopiaOculta, titulo, mensagem, html);
        }
        
        public System.Threading.Tasks.Task<bool> EnviarAsync(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html) {
            return base.Channel.EnviarAsync(emailPara, emailCopia, emailCopiaOculta, titulo, mensagem, html);
        }
        
        public MonitorTrocasSuprimentosII.wsEmail.sEmail[] Listar() {
            return base.Channel.Listar();
        }
        
        public System.Threading.Tasks.Task<MonitorTrocasSuprimentosII.wsEmail.sEmail[]> ListarAsync() {
            return base.Channel.ListarAsync();
        }
        
        public string EmailUsuario(string SerieEquipamento) {
            return base.Channel.EmailUsuario(SerieEquipamento);
        }
        
        public System.Threading.Tasks.Task<string> EmailUsuarioAsync(string SerieEquipamento) {
            return base.Channel.EmailUsuarioAsync(SerieEquipamento);
        }
    }
}
