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

namespace Monitor.Ocomon {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ChamadosSoap", Namespace="http://tempuri.org/")]
    public partial class Chamados : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback retornaChamadosOperationCompleted;
        
        private System.Threading.SendOrPostCallback retornaChamadoIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback FecharOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Chamados() {
            this.Url = global::Monitor.Properties.Settings.Default.Monitor_Ocomon_Chamados;
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
        public event retornaChamadosCompletedEventHandler retornaChamadosCompleted;
        
        /// <remarks/>
        public event retornaChamadoIdCompletedEventHandler retornaChamadoIdCompleted;
        
        /// <remarks/>
        public event FecharCompletedEventHandler FecharCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/retornaChamados", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Req[] retornaChamados() {
            object[] results = this.Invoke("retornaChamados", new object[0]);
            return ((Req[])(results[0]));
        }
        
        /// <remarks/>
        public void retornaChamadosAsync() {
            this.retornaChamadosAsync(null);
        }
        
        /// <remarks/>
        public void retornaChamadosAsync(object userState) {
            if ((this.retornaChamadosOperationCompleted == null)) {
                this.retornaChamadosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretornaChamadosOperationCompleted);
            }
            this.InvokeAsync("retornaChamados", new object[0], this.retornaChamadosOperationCompleted, userState);
        }
        
        private void OnretornaChamadosOperationCompleted(object arg) {
            if ((this.retornaChamadosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.retornaChamadosCompleted(this, new retornaChamadosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/retornaChamadoId", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Req retornaChamadoId(int idReq) {
            object[] results = this.Invoke("retornaChamadoId", new object[] {
                        idReq});
            return ((Req)(results[0]));
        }
        
        /// <remarks/>
        public void retornaChamadoIdAsync(int idReq) {
            this.retornaChamadoIdAsync(idReq, null);
        }
        
        /// <remarks/>
        public void retornaChamadoIdAsync(int idReq, object userState) {
            if ((this.retornaChamadoIdOperationCompleted == null)) {
                this.retornaChamadoIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretornaChamadoIdOperationCompleted);
            }
            this.InvokeAsync("retornaChamadoId", new object[] {
                        idReq}, this.retornaChamadoIdOperationCompleted, userState);
        }
        
        private void OnretornaChamadoIdOperationCompleted(object arg) {
            if ((this.retornaChamadoIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.retornaChamadoIdCompleted(this, new retornaChamadoIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Fechar", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Fechar(string numero, string etiqueta, string postagem, string usuario, string serie) {
            object[] results = this.Invoke("Fechar", new object[] {
                        numero,
                        etiqueta,
                        postagem,
                        usuario,
                        serie});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void FecharAsync(string numero, string etiqueta, string postagem, string usuario, string serie) {
            this.FecharAsync(numero, etiqueta, postagem, usuario, serie, null);
        }
        
        /// <remarks/>
        public void FecharAsync(string numero, string etiqueta, string postagem, string usuario, string serie, object userState) {
            if ((this.FecharOperationCompleted == null)) {
                this.FecharOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFecharOperationCompleted);
            }
            this.InvokeAsync("Fechar", new object[] {
                        numero,
                        etiqueta,
                        postagem,
                        usuario,
                        serie}, this.FecharOperationCompleted, userState);
        }
        
        private void OnFecharOperationCompleted(object arg) {
            if ((this.FecharCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FecharCompleted(this, new FecharCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Req {
        
        private string numeroField;
        
        private string localField;
        
        private string serieField;
        
        private string descricaoField;
        
        private string tonerField;
        
        private string fotoreceptorField;
        
        private string dataAberturaField;
        
        private string qtdDiasField;
        
        private string tpEnvioField;
        
        private string nomeField;
        
        private string codUSDField;
        
        private string emailUsuarioField;
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
            }
        }
        
        /// <remarks/>
        public string Local {
            get {
                return this.localField;
            }
            set {
                this.localField = value;
            }
        }
        
        /// <remarks/>
        public string Serie {
            get {
                return this.serieField;
            }
            set {
                this.serieField = value;
            }
        }
        
        /// <remarks/>
        public string Descricao {
            get {
                return this.descricaoField;
            }
            set {
                this.descricaoField = value;
            }
        }
        
        /// <remarks/>
        public string Toner {
            get {
                return this.tonerField;
            }
            set {
                this.tonerField = value;
            }
        }
        
        /// <remarks/>
        public string Fotoreceptor {
            get {
                return this.fotoreceptorField;
            }
            set {
                this.fotoreceptorField = value;
            }
        }
        
        /// <remarks/>
        public string DataAbertura {
            get {
                return this.dataAberturaField;
            }
            set {
                this.dataAberturaField = value;
            }
        }
        
        /// <remarks/>
        public string QtdDias {
            get {
                return this.qtdDiasField;
            }
            set {
                this.qtdDiasField = value;
            }
        }
        
        /// <remarks/>
        public string TpEnvio {
            get {
                return this.tpEnvioField;
            }
            set {
                this.tpEnvioField = value;
            }
        }
        
        /// <remarks/>
        public string Nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
            }
        }
        
        /// <remarks/>
        public string CodUSD {
            get {
                return this.codUSDField;
            }
            set {
                this.codUSDField = value;
            }
        }
        
        /// <remarks/>
        public string EmailUsuario {
            get {
                return this.emailUsuarioField;
            }
            set {
                this.emailUsuarioField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void retornaChamadosCompletedEventHandler(object sender, retornaChamadosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class retornaChamadosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal retornaChamadosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Req[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Req[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void retornaChamadoIdCompletedEventHandler(object sender, retornaChamadoIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class retornaChamadoIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal retornaChamadoIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Req Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Req)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void FecharCompletedEventHandler(object sender, FecharCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FecharCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FecharCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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