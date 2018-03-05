using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace VCapsParser
{
    public static class VCapsEnvParser
    {
        public static string GetConnectionString(ServiceType serviceType)
        {
            string connectionString = string.Empty;
            switch (serviceType)
            {
                case ServiceType.MySql:
                    connectionString = GetCrendentialsforDb();
                    break;
                default:
                    break;
            }
            return connectionString;
        }

        public static string GetRawData(string attributeName)
        {
            string rawString = string.Empty;
            var strVcapServices = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            if (!String.IsNullOrEmpty(strVcapServices))
            {
                JToken vcapObject = JObject.Parse(strVcapServices);
                if (vcapObject[attributeName] != null )
                {
                    rawString = Convert.ToString(vcapObject[attributeName]);
                }
            }
            return rawString;
        }

        /// <summary>
        /// This is to call GetCrendentialsforDB to get credentials based on service type
        /// </summary>  
        private static string GetCrendentialsforDb()
        {
            string connectionString = string.Empty;
            var strVcapServices = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            if (!String.IsNullOrEmpty(strVcapServices))
            {
                JToken vcapObject = JObject.Parse(strVcapServices);
                if (vcapObject["p-mysql"] != null && vcapObject["p-mysql"].Count() > 0)
                {
                    connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                               Convert.ToString(vcapObject["p-mysql"][0]["credentials"]["hostname"]), Convert.ToString(vcapObject["p-mysql"][0]["credentials"]["name"]), Convert.ToString(vcapObject["p-mysql"][0]["credentials"]["username"]), Convert.ToString(vcapObject["p-mysql"][0]["credentials"]["password"]), Convert.ToString(vcapObject["p-mysql"][0]["credentials"]["port"]));
                }
                else if (vcapObject["user-provided"] != null)
                {
                    for (int counter = 0; counter < vcapObject["user-provided"].Count(); counter++)
                    {
                        if (Convert.ToString(vcapObject["user-provided"][counter]["name"]).Contains("postgre") && !String.IsNullOrEmpty(Convert.ToString(vcapObject["user-provided"][counter]["credentials"]["hostname"])))
                        {
                            connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                                Convert.ToString(vcapObject["user-provided"][counter]["credentials"]["hostname"]), Convert.ToString(vcapObject["user-provided"][counter]["credentials"]["db"]), Convert.ToString(vcapObject["user-provided"][counter]["credentials"]["username"]), Convert.ToString(vcapObject["user-provided"][counter]["credentials"]["password"]), Convert.ToString(vcapObject["user-provided"][counter]["credentials"]["port"]));
                            break;
                        }
                    }
                }
                else if (vcapObject["elephantsql"] != null)
                {
                    connectionString = Convert.ToString(vcapObject["elephantsql"][0]["credentials"]["uri"]);
                }
            }
            return connectionString;
        }

    }
}
