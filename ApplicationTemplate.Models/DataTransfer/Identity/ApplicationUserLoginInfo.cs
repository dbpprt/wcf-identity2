namespace ApplicationTemplate.Models.DataTransfer
{
    public sealed class ApplicationUserLoginInfo
    {
        public string LoginProvider
        {
            get;
            set;
        }

        public string ProviderKey
        {
            get;
            set;
        }

        public ApplicationUserLoginInfo()
        {
            
        }

        public ApplicationUserLoginInfo(string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}
