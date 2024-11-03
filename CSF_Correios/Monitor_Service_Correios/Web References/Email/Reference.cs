﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Monitor_Service_Correios.Email {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EmailSoap", Namespace="http://tempuri.org/")]
    public partial class Email : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EnviarOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnviarHtmlMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnviarHtmlMessageCopiaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Email() {
            this.Url = global::Monitor_Service_Correios.Properties.Settings.Default.Monitor_Service_Correios_Email_Email;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event EnviarCompletedEventHandler EnviarCompleted;
        
        /// <remarks/>
        public event EnviarHtmlMessageCompletedEventHandler EnviarHtmlMessageCompleted;
        
        /// <remarks/>
        public event EnviarHtmlMessageCopiaCompletedEventHandler EnviarHtmlMessageCopiaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Enviar", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Enviar(string para, string assunto, string mensagem, string sistema) {
            this.Invoke("Enviar", new object[] {
                        para,
                        assunto,
                        mensagem,
                        sistema});
        }
        
        /// <remarks/>
        public void EnviarAsync(string para, string assunto, string mensagem, string sistema) {
            this.EnviarAsync(para, assunto, mensagem, sistema, null);
        }
        
        /// <remarks/>
        public void EnviarAsync(string para, string assunto, string mensagem, string sistema, object userState) {
            if ((this.EnviarOperationCompleted == null)) {
                this.EnviarOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarOperationCompleted);
            }
            this.InvokeAsync("Enviar", new object[] {
                        para,
                        assunto,
                        mensagem,
                        sistema}, this.EnviarOperationCompleted, userState);
        }
        
        private void OnEnviarOperationCompleted(object arg) {
            if ((this.EnviarCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarHtmlMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool EnviarHtmlMessage(string para, string assunto, string mensagem, string sistema, bool html) {
            object[] results = this.Invoke("EnviarHtmlMessage", new object[] {
                        para,
                        assunto,
                        mensagem,
                        sistema,
                        html});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarHtmlMessageAsync(string para, string assunto, string mensagem, string sistema, bool html) {
            this.EnviarHtmlMessageAsync(para, assunto, mensagem, sistema, html, null);
        }
        
        /// <remarks/>
        public void EnviarHtmlMessageAsync(string para, string assunto, string mensagem, string sistema, bool html, object userState) {
            if ((this.EnviarHtmlMessageOperationCompleted == null)) {
                this.EnviarHtmlMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarHtmlMessageOperationCompleted);
            }
            this.InvokeAsync("EnviarHtmlMessage", new object[] {
                        para,
                        assunto,
                        mensagem,
                        sistema,
                        html}, this.EnviarHtmlMessageOperationCompleted, userState);
        }
        
        private void OnEnviarHtmlMessageOperationCompleted(object arg) {
            if ((this.EnviarHtmlMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarHtmlMessageCompleted(this, new EnviarHtmlMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarHtmlMessageCopia", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool EnviarHtmlMessageCopia(string para, string copia, string assunto, string mensagem, string sistema, bool html) {
            object[] results = this.Invoke("EnviarHtmlMessageCopia", new object[] {
                        para,
                        copia,
                        assunto,
                        mensagem,
                        sistema,
                        html});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarHtmlMessageCopiaAsync(string para, string copia, string assunto, string mensagem, string sistema, bool html) {
            this.EnviarHtmlMessageCopiaAsync(para, copia, assunto, mensagem, sistema, html, null);
        }
        
        /// <remarks/>
        public void EnviarHtmlMessageCopiaAsync(string para, string copia, string assunto, string mensagem, string sistema, bool html, object userState) {
            if ((this.EnviarHtmlMessageCopiaOperationCompleted == null)) {
                this.EnviarHtmlMessageCopiaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarHtmlMessageCopiaOperationCompleted);
            }
            this.InvokeAsync("EnviarHtmlMessageCopia", new object[] {
                        para,
                        copia,
                        assunto,
                        mensagem,
                        sistema,
                        html}, this.EnviarHtmlMessageCopiaOperationCompleted, userState);
        }
        
        private void OnEnviarHtmlMessageCopiaOperationCompleted(object arg) {
            if ((this.EnviarHtmlMessageCopiaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarHtmlMessageCopiaCompleted(this, new EnviarHtmlMessageCopiaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void EnviarCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void EnviarHtmlMessageCompletedEventHandler(object sender, EnviarHtmlMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarHtmlMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarHtmlMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void EnviarHtmlMessageCopiaCompletedEventHandler(object sender, EnviarHtmlMessageCopiaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarHtmlMessageCopiaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarHtmlMessageCopiaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591