namespace WPF_N_Tier_Test.Modules.Common
{
    public enum GlobalMessageType
    {
        Status,
        Error,
    }
    public class GlobalMessageStore
    {
        private string _currentMessage = string.Empty;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set
            {
                _currentMessage = value;
                OnMessageChanged(_currentMessage);
            }
        }
        public GlobalMessageType _currentMessageType = GlobalMessageType.Status;
        public GlobalMessageType CurrentMessageType
        {
            get { return _currentMessageType; }
            set
            {
                if (_currentMessageType != value)
                {
                    _currentMessageType = value;
                    OnMessageTypeChanged(_currentMessageType);
                }
            }
        }



        #region Events
        public event EventHandler<string>? CurrentMessageChanged;
        protected virtual void OnMessageChanged(string e)
        {
            // copy to avoid race condition
            EventHandler<string>? handler = CurrentMessageChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler<GlobalMessageType>? MessageTypeChanged;
        protected virtual void OnMessageTypeChanged(GlobalMessageType e)
        {
            // copy to avoid race condition
            EventHandler<GlobalMessageType>? handler = MessageTypeChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        public void SetCurrentMessage(string currentMessage, GlobalMessageType messageType = GlobalMessageType.Status)
        {
            CurrentMessage = currentMessage;
            CurrentMessageType = messageType;
        }
    }
}
