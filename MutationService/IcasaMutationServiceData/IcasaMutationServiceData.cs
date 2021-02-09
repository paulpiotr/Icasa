using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;

/* Niescalona zmiana z projektu „IcasaMutationServiceData (netstandard2.1)”
Przed:
using System.Threading.Tasks;
using System.Text;
using SoapHttpClient;
Po:
using System.Text;
using System.Threading.Tasks;
*/

/* Niescalona zmiana z projektu „IcasaMutationServiceData (net5.0)”
Przed:
using System.Threading.Tasks;
using System.Text;
using SoapHttpClient;
Po:
using System.Text;
using System.Threading.Tasks;
*/
using System.Text;
using System.Threading.Tasks;
/* Niescalona zmiana z projektu „IcasaMutationServiceData (netstandard2.1)”
Przed:
using SoapHttpClient.Enums;
Po:
using SoapHttpClient;
using SoapHttpClient.Enums;
*/

/* Niescalona zmiana z projektu „IcasaMutationServiceData (net5.0)”
Przed:
using SoapHttpClient.Enums;
Po:
using SoapHttpClient;
using SoapHttpClient.Enums;
*/


namespace IcasaMutationServiceData
{
    public class IcasaMutationServiceData
    {
        private static readonly log4net.ILog Log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);

        #region private static readonly string EndpointAddress
        /// <summary>
        /// Adres punktu końcowego jako string, tylko do odczytu pobierany z pliku konfiguracyjnego
        /// The endpoint address as a read-only string, taken from the configuration file
        /// </summary>
#if DEBUG
        private static readonly string EndpointAddress = NetAppCommon.Configuration.GetValue<string>("icasa.mutation.service.data.appsettings.debug.json", "EndpointAddress");
#else
        private static readonly string EndpointAddress = NetAppCommon.Configuration.GetValue<string>("icasa.mutation.service.data.appsettings.release.json", "EndpointAddress");
#endif
        #endregion

        #region private static readonly string MutationServiceClientCredentialsUserName
        /// <summary>
        /// Adres punktu końcowego jako string, tylko do odczytu pobierany z pliku konfiguracyjnego
        /// The endpoint address as a read-only string, taken from the configuration file
        /// </summary>
#if DEBUG
        private static readonly string MutationServiceClientCredentialsUserName = NetAppCommon.Configuration.GetValue<string>("icasa.mutation.service.data.appsettings.debug.json", "MutationServiceClientCredentialsUserName");
#else
        private static readonly string MutationServiceClientCredentialsUserName = NetAppCommon.Configuration.GetValue<string>("icasa.mutation.service.data.appsettings.release.json", "MutationServiceClientCredentialsUserName");
#endif
        #endregion

        #region private static readonly string MutationServiceClientCredentialsPassword
        /// <summary>
        /// Adres punktu końcowego jako string, tylko do odczytu pobierany z pliku konfiguracyjnego
        /// The endpoint address as a read-only string, taken from the configuration file
        /// </summary>
#if DEBUG
        private static readonly string MutationServiceClientCredentialsPassword = NetAppCommon.Configuration.GetValue<string>("icasa.mutation.service.data.appsettings.debug.json", "MutationServiceClientCredentialsPassword");
#else
        private static readonly string MutationServiceClientCredentialsPassword = NetAppCommon.Configuration.GetValue<string>("icasa.mutation.service.data.appsettings.release.json", "MutationServiceClientCredentialsPassword");
#endif
        #endregion

        #region public static BasicHttpBinding GetBasicHttpBinding()
        public static BasicHttpBinding GetBasicHttpBinding()
        {
            try
            {
                return new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly)
                {
                    //MessageEncoding = WSMessageEncoding.Text,
                    Security = {
                        Transport =
                        {
                            ClientCredentialType = HttpClientCredentialType.Basic,
                            ProxyCredentialType = HttpProxyCredentialType.Basic
                        },
                        Message =
                        {
                            ClientCredentialType = BasicHttpMessageCredentialType.UserName,
                        },
                        Mode = BasicHttpSecurityMode.Transport,
                    },
                    //HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                    MaxReceivedMessageSize = 655536,
                    ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
                    {
                        MaxArrayLength = 655536,
                        MaxStringContentLength = 655536,
                    },
                    SendTimeout = TimeSpan.MaxValue,
                    OpenTimeout = TimeSpan.MaxValue,
                    ReceiveTimeout = TimeSpan.MaxValue,
                };
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static WSHttpBinding GetWSHttpBinding()
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public static WSHttpBinding GetWSHttpBinding()
        {
            try
            {
                var wSHttpBinding = new WSHttpBinding();
                wSHttpBinding.Security.Mode = SecurityMode.Transport;
                wSHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                //wSHttpBinding.EnvelopeVersion = EnvelopeVersion.Soap11;
                //wSHttpBinding.MessageVersion = MessageVersion.Soap12WSAddressing10;
                wSHttpBinding.MaxBufferPoolSize = int.MaxValue;
                wSHttpBinding.MaxReceivedMessageSize = int.MaxValue;
                wSHttpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                wSHttpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                wSHttpBinding.ReaderQuotas.MaxDepth = int.MaxValue;
                wSHttpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                wSHttpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                wSHttpBinding.AllowCookies = true;
                return wSHttpBinding;
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static CustomBinding GetCustomBinding()
        public static CustomBinding GetCustomBinding()
        {
            try
            {
                var customBinding = new CustomBinding();
                TransportSecurityBindingElement userNameOverTransportSecurityBindingElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
                userNameOverTransportSecurityBindingElement.MessageSecurityVersion = System.ServiceModel.MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
                userNameOverTransportSecurityBindingElement.IncludeTimestamp = false;
                customBinding.Elements.Add(userNameOverTransportSecurityBindingElement);
                var textBindingElement = new TextMessageEncodingBindingElement();
                customBinding.Elements.Add(textBindingElement);
                var httpsBindingElement = new HttpsTransportBindingElement
                {
                    AllowCookies = true,
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue
                };
                //httpsBindingElement.AuthenticationScheme = System.Net.AuthenticationSchemes.Basic;
                customBinding.Elements.Add(httpsBindingElement);
                //SecurityBindingElement securityBindingElement = SecurityBindingElement.
                //includeTimestamp
                //messageProtectionOrder =
                //customBinding.Elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
                return customBinding;
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public static IcasaMutationServiceReference.MutationServiceClient IcasaMutationServiceClient()
        /// <summary>
        /// Skonfiguruj, autoryzuj i pobierz klienta do serwisu ICASA
        /// Set up, authorize and download the client to the ICASA service
        /// </summary>
        /// <returns>
        /// Klient serwisu ICASA jako IcasaMutationServiceReference.MutationServiceClient
        /// ICASA client as IcasaMutationServiceReference.MutationServiceClient
        /// </returns>
        public IcasaMutationServiceReference.MutationServiceClient GetIcasaMutationServiceClient()
        {
            try
            {
                BasicHttpBinding basicHttpBinding = GetBasicHttpBinding();
                WSHttpBinding wSHttpBinding = GetWSHttpBinding();
                CustomBinding customBinding = GetCustomBinding();
                var endpointAddress = new EndpointAddress(EndpointAddress);
                var mutationServiceClient = new IcasaMutationServiceReference.MutationServiceClient(wSHttpBinding, endpointAddress);
                mutationServiceClient.Endpoint.Address = endpointAddress;
                mutationServiceClient.ClientCredentials.UserName.UserName = MutationServiceClientCredentialsUserName;
                mutationServiceClient.ClientCredentials.UserName.Password = MutationServiceClientCredentialsPassword;
                mutationServiceClient.Endpoint.EndpointBehaviors.Add(new NetAppCommon.Logging.SoapEndpointBehavior());
                var operationContextScope = new OperationContextScope(mutationServiceClient.InnerChannel);
                var httpRequestMessageProperty = new HttpRequestMessageProperty();
                var authenticationHeaderToBase64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", MutationServiceClientCredentialsUserName, MutationServiceClientCredentialsPassword)));
                httpRequestMessageProperty.Headers.Add("Authorization", $"Basic { authenticationHeaderToBase64String }");
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestMessageProperty;
                return mutationServiceClient;
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        //#region public static IcasaMutationServiceReference.MutationServiceClient IcasaMutationServiceClientN()
        ///// <summary>
        ///// Skonfiguruj, autoryzuj i pobierz klienta do serwisu ICASA
        ///// Set up, authorize and download the client to the ICASA service
        ///// </summary>
        ///// <returns>
        ///// Klient serwisu ICASA jako IcasaMutationServiceReference.MutationServiceClient
        ///// ICASA client as IcasaMutationServiceReference.MutationServiceClient
        ///// </returns>
        //public static IcasaMutationServiceN.MutationServiceClient GetIcasaMutationServiceClientN()
        //{
        //    try
        //    {
        //        //BasicHttpBinding basicHttpBinding = GetBasicHttpBinding();
        //        //WSHttpBinding wSHttpBinding = GetWSHttpBinding();
        //        //CustomBinding customBinding = GetCustomBinding();
        //        EndpointAddress endpointAddress = new EndpointAddress(EndpointAddress);
        //        //IcasaMutationServiceN.MutationServiceClient mutationServiceClient = new IcasaMutationServiceN.MutationServiceClient(wSHttpBinding, endpointAddress);
        //        IcasaMutationServiceN.MutationServiceClient mutationServiceClient = new IcasaMutationServiceN.MutationServiceClient();
        //        mutationServiceClient.Endpoint.Address = endpointAddress;
        //        mutationServiceClient.ClientCredentials.UserName.UserName = MutationServiceClientCredentialsUserName;
        //        mutationServiceClient.ClientCredentials.UserName.Password = MutationServiceClientCredentialsPassword;
        //        mutationServiceClient.Endpoint.EndpointBehaviors.Add(new NetAppCommon.Logging.SoapEndpointBehavior());
        //        //OperationContextScope operationContextScope = new OperationContextScope(mutationServiceClient.InnerChannel);
        //        //HttpRequestMessageProperty httpRequestMessageProperty = new HttpRequestMessageProperty();
        //        //string authenticationHeaderToBase64String = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", MutationServiceClientCredentialsUserName, MutationServiceClientCredentialsPassword)));
        //        //httpRequestMessageProperty.Headers.Add("Authorization", $"Basic { authenticationHeaderToBase64String }");
        //        //OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestMessageProperty;
        //        return mutationServiceClient;
        //    }
        //    catch (Exception e)
        //    {
        //        log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
        //    }
        //    return null;
        //}
        //#endregion

        #region public static async Task TestAsync()

        /* Niescalona zmiana z projektu „IcasaMutationServiceData (net5.0)”
        Przed:
                public async Task TestAsync()
        Po:
                public async Task Test()
        */

        /* Niescalona zmiana z projektu „IcasaMutationServiceData (netstandard2.1)”
        Przed:
                public async Task TestAsync()
        Po:
                public async Task Test()
        */
        public void Test()
        {
            try
            {
                Console.WriteLine("Begin");
                IcasaMutationServiceReference.MutationServiceClient mutationServiceClient = GetIcasaMutationServiceClient();
                if (null != mutationServiceClient)
                {
                    var storesRequest = new IcasaMutationServiceReference.GetStoresRequest();
                    Task<IcasaMutationServiceReference.GetStoresResponse> stores = mutationServiceClient.GetStoresAsync(storesRequest);
                    Console.WriteLine(stores.Result.GetStoresResult.OperationSuccess);
                    Console.WriteLine(stores.Result.GetStoresResult.Data.Length);
                    foreach (IcasaMutationServiceReference.StoreOut store in stores.Result.GetStoresResult.Data)
                    {
                        Console.WriteLine(store.CAIC);
                    }

                    var blockDebtorRequest = new IcasaMutationServiceReference.BlockDebtorRequest
                    {
                        DebtorExternalID = "",
                        Notes = "Notes"
                    };
                    Task<IcasaMutationServiceReference.BlockDebtorResponse> blockDebtorResponse = mutationServiceClient.BlockDebtorAsync(blockDebtorRequest);
                    Console.WriteLine(blockDebtorResponse.Result.BlockDebtorResult.ErrorType);

                }

                Console.WriteLine("End");
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
                if (null != e.InnerException)
                {
                    Log4net.Error(string.Format("{0}, {1}", e.InnerException.Message, e.InnerException.StackTrace), e.InnerException);
                    if (null != e.InnerException.InnerException)
                    {
                        Log4net.Error(string.Format("{0}, {1}", e.InnerException.InnerException.Message, e.InnerException.InnerException.StackTrace), e.InnerException.InnerException);
                    }
                }
            }
        }
        #endregion
    }
}
