using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace razorWebApplication1.Utils
{
    public class ConfigTest
    {
        public string GetConfigValue(string key)
        {
            // 1. IConfigurationBuilder 생성
            var builder = new ConfigurationBuilder();
            // 2. Azure App Configuration 연결
            builder.AddAzureAppConfiguration(options =>
            {
                // App Configuration 연결 문자열 또는 Managed Identity 사용 가능
                //options.Connect("Endpoint=https://<your-appconfig-name>.azconfig.io;Id=<id>;Secret=<secret>")
                options.Connect("Endpoint=")
                    // Key-Value 및 Label 필터
                    .Select(KeyFilter.Any, LabelFilter.Null)
                    // Key Vault 참조 자동 해석
                    .ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });
            });
            var configuration = builder.Build();

            // 3. Key-Value 읽기
            string value = configuration[key];
            Console.WriteLine($"Config Value for '{key}': {value}");
            return value;
        }


    }
}
