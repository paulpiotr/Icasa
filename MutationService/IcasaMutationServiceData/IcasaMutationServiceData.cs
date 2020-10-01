using System;
using System.Reflection;

namespace IcasaMutationServiceData
{
    public class IcasaMutationServiceData
    {
        private static readonly log4net.ILog _log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Test()
        {
            try
            {
                Console.WriteLine("Begin");
                IcasaMutationService.MutationServiceClient mutationServiceClient = new IcasaMutationService.MutationServiceClient();
                mutationServiceClient.OpenAsync();
                IcasaMutationService.CompanyFilter companyFilter = new IcasaMutationService.CompanyFilter()
                {
                    LikeCountry = "Polska",
                };
                IcasaMutationService.CompanyRequestResult companyRequestResult = mutationServiceClient.GetCompaniesAsync(companyFilter).Result;
                Console.WriteLine(companyRequestResult.Data.Count);
                mutationServiceClient.CloseAsync();
                Console.WriteLine("End");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("{0}, {1}", e.Message, e.StackTrace));
                _log4net.Error(string.Format("{0}, {1}", e.Message, e.StackTrace), e);
            }
        }
    }
}
