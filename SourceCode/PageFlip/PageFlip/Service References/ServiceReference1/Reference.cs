﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 5.0.61118.0
// 
namespace PageFlip.ServiceReference1 {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EMP_BASIC_INFO", Namespace="http://schemas.datacontract.org/2004/07/PageFlip.Web")]
    public partial class EMP_BASIC_INFO : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string EMP_CONTACTField;
        
        private string EMP_EMAIL_IDField;
        
        private string EMP_FIRST_NAMEField;
        
        private long EMP_IDField;
        
        private string EMP_LAST_NAMEField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMP_CONTACT {
            get {
                return this.EMP_CONTACTField;
            }
            set {
                if ((object.ReferenceEquals(this.EMP_CONTACTField, value) != true)) {
                    this.EMP_CONTACTField = value;
                    this.RaisePropertyChanged("EMP_CONTACT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMP_EMAIL_ID {
            get {
                return this.EMP_EMAIL_IDField;
            }
            set {
                if ((object.ReferenceEquals(this.EMP_EMAIL_IDField, value) != true)) {
                    this.EMP_EMAIL_IDField = value;
                    this.RaisePropertyChanged("EMP_EMAIL_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMP_FIRST_NAME {
            get {
                return this.EMP_FIRST_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.EMP_FIRST_NAMEField, value) != true)) {
                    this.EMP_FIRST_NAMEField = value;
                    this.RaisePropertyChanged("EMP_FIRST_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long EMP_ID {
            get {
                return this.EMP_IDField;
            }
            set {
                if ((this.EMP_IDField.Equals(value) != true)) {
                    this.EMP_IDField = value;
                    this.RaisePropertyChanged("EMP_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMP_LAST_NAME {
            get {
                return this.EMP_LAST_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.EMP_LAST_NAMEField, value) != true)) {
                    this.EMP_LAST_NAMEField = value;
                    this.RaisePropertyChanged("EMP_LAST_NAME");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Article", Namespace="http://schemas.datacontract.org/2004/07/PageFlip.Web")]
    public partial class Article : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int ArticleIDField;
        
        private System.Nullable<int> CatIDField;
        
        private string ContentField;
        
        private int CreatedByUserField;
        
        private System.DateTime CreatedDateField;
        
        private string ImageField;
        
        private string SummaryField;
        
        private string TitleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ArticleID {
            get {
                return this.ArticleIDField;
            }
            set {
                if ((this.ArticleIDField.Equals(value) != true)) {
                    this.ArticleIDField = value;
                    this.RaisePropertyChanged("ArticleID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> CatID {
            get {
                return this.CatIDField;
            }
            set {
                if ((this.CatIDField.Equals(value) != true)) {
                    this.CatIDField = value;
                    this.RaisePropertyChanged("CatID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CreatedByUser {
            get {
                return this.CreatedByUserField;
            }
            set {
                if ((this.CreatedByUserField.Equals(value) != true)) {
                    this.CreatedByUserField = value;
                    this.RaisePropertyChanged("CreatedByUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((this.CreatedDateField.Equals(value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Summary {
            get {
                return this.SummaryField;
            }
            set {
                if ((object.ReferenceEquals(this.SummaryField, value) != true)) {
                    this.SummaryField = value;
                    this.RaisePropertyChanged("Summary");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ServiceReference1.Service1")]
    public interface Service1 {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:Service1/DoWork", ReplyAction="urn:Service1/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:Service1/GetAllEmps", ReplyAction="urn:Service1/GetAllEmpsResponse")]
        System.IAsyncResult BeginGetAllEmps(System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO> EndGetAllEmps(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:Service1/Get_AllArticles", ReplyAction="urn:Service1/Get_AllArticlesResponse")]
        System.IAsyncResult BeginGet_AllArticles(System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article> EndGet_AllArticles(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Service1Channel : PageFlip.ServiceReference1.Service1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetAllEmpsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetAllEmpsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Get_AllArticlesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public Get_AllArticlesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<PageFlip.ServiceReference1.Service1>, PageFlip.ServiceReference1.Service1 {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetAllEmpsDelegate;
        
        private EndOperationDelegate onEndGetAllEmpsDelegate;
        
        private System.Threading.SendOrPostCallback onGetAllEmpsCompletedDelegate;
        
        private BeginOperationDelegate onBeginGet_AllArticlesDelegate;
        
        private EndOperationDelegate onEndGet_AllArticlesDelegate;
        
        private System.Threading.SendOrPostCallback onGet_AllArticlesCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> DoWorkCompleted;
        
        public event System.EventHandler<GetAllEmpsCompletedEventArgs> GetAllEmpsCompleted;
        
        public event System.EventHandler<Get_AllArticlesCompletedEventArgs> Get_AllArticlesCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult PageFlip.ServiceReference1.Service1.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void PageFlip.ServiceReference1.Service1.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((PageFlip.ServiceReference1.Service1)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((PageFlip.ServiceReference1.Service1)(this)).EndDoWork(result);
            return null;
        }
        
        private void OnDoWorkCompleted(object state) {
            if ((this.DoWorkCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DoWorkCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DoWorkAsync() {
            this.DoWorkAsync(null);
        }
        
        public void DoWorkAsync(object userState) {
            if ((this.onBeginDoWorkDelegate == null)) {
                this.onBeginDoWorkDelegate = new BeginOperationDelegate(this.OnBeginDoWork);
            }
            if ((this.onEndDoWorkDelegate == null)) {
                this.onEndDoWorkDelegate = new EndOperationDelegate(this.OnEndDoWork);
            }
            if ((this.onDoWorkCompletedDelegate == null)) {
                this.onDoWorkCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDoWorkCompleted);
            }
            base.InvokeAsync(this.onBeginDoWorkDelegate, null, this.onEndDoWorkDelegate, this.onDoWorkCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult PageFlip.ServiceReference1.Service1.BeginGetAllEmps(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetAllEmps(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO> PageFlip.ServiceReference1.Service1.EndGetAllEmps(System.IAsyncResult result) {
            return base.Channel.EndGetAllEmps(result);
        }
        
        private System.IAsyncResult OnBeginGetAllEmps(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((PageFlip.ServiceReference1.Service1)(this)).BeginGetAllEmps(callback, asyncState);
        }
        
        private object[] OnEndGetAllEmps(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO> retVal = ((PageFlip.ServiceReference1.Service1)(this)).EndGetAllEmps(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetAllEmpsCompleted(object state) {
            if ((this.GetAllEmpsCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetAllEmpsCompleted(this, new GetAllEmpsCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetAllEmpsAsync() {
            this.GetAllEmpsAsync(null);
        }
        
        public void GetAllEmpsAsync(object userState) {
            if ((this.onBeginGetAllEmpsDelegate == null)) {
                this.onBeginGetAllEmpsDelegate = new BeginOperationDelegate(this.OnBeginGetAllEmps);
            }
            if ((this.onEndGetAllEmpsDelegate == null)) {
                this.onEndGetAllEmpsDelegate = new EndOperationDelegate(this.OnEndGetAllEmps);
            }
            if ((this.onGetAllEmpsCompletedDelegate == null)) {
                this.onGetAllEmpsCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetAllEmpsCompleted);
            }
            base.InvokeAsync(this.onBeginGetAllEmpsDelegate, null, this.onEndGetAllEmpsDelegate, this.onGetAllEmpsCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult PageFlip.ServiceReference1.Service1.BeginGet_AllArticles(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGet_AllArticles(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article> PageFlip.ServiceReference1.Service1.EndGet_AllArticles(System.IAsyncResult result) {
            return base.Channel.EndGet_AllArticles(result);
        }
        
        private System.IAsyncResult OnBeginGet_AllArticles(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((PageFlip.ServiceReference1.Service1)(this)).BeginGet_AllArticles(callback, asyncState);
        }
        
        private object[] OnEndGet_AllArticles(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article> retVal = ((PageFlip.ServiceReference1.Service1)(this)).EndGet_AllArticles(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGet_AllArticlesCompleted(object state) {
            if ((this.Get_AllArticlesCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.Get_AllArticlesCompleted(this, new Get_AllArticlesCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void Get_AllArticlesAsync() {
            this.Get_AllArticlesAsync(null);
        }
        
        public void Get_AllArticlesAsync(object userState) {
            if ((this.onBeginGet_AllArticlesDelegate == null)) {
                this.onBeginGet_AllArticlesDelegate = new BeginOperationDelegate(this.OnBeginGet_AllArticles);
            }
            if ((this.onEndGet_AllArticlesDelegate == null)) {
                this.onEndGet_AllArticlesDelegate = new EndOperationDelegate(this.OnEndGet_AllArticles);
            }
            if ((this.onGet_AllArticlesCompletedDelegate == null)) {
                this.onGet_AllArticlesCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGet_AllArticlesCompleted);
            }
            base.InvokeAsync(this.onBeginGet_AllArticlesDelegate, null, this.onEndGet_AllArticlesDelegate, this.onGet_AllArticlesCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override PageFlip.ServiceReference1.Service1 CreateChannel() {
            return new Service1ClientChannel(this);
        }
        
        private class Service1ClientChannel : ChannelBase<PageFlip.ServiceReference1.Service1>, PageFlip.ServiceReference1.Service1 {
            
            public Service1ClientChannel(System.ServiceModel.ClientBase<PageFlip.ServiceReference1.Service1> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("DoWork", _args, callback, asyncState);
                return _result;
            }
            
            public void EndDoWork(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("DoWork", _args, result);
            }
            
            public System.IAsyncResult BeginGetAllEmps(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetAllEmps", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO> EndGetAllEmps(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO> _result = ((System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.EMP_BASIC_INFO>)(base.EndInvoke("GetAllEmps", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGet_AllArticles(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("Get_AllArticles", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article> EndGet_AllArticles(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article> _result = ((System.Collections.ObjectModel.ObservableCollection<PageFlip.ServiceReference1.Article>)(base.EndInvoke("Get_AllArticles", _args, result)));
                return _result;
            }
        }
    }
}
