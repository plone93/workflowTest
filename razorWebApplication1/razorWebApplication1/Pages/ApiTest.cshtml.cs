using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorWebApplication1.Utils;

namespace razorWebApplication1.Pages
{
    public class ApiTestModel : PageModel
    {
        public string OracleDbSecretValue { get; set; }
        public string OracleDbSecretValue2 { get; set; }
        public string OracleDbSecretValue3 { get; set; }

        public async Task OnGet()
        {

            // Fetch secret from Azure Key Vault
            var keyVault = new KeyVaultTest();
            var result = await keyVault.GetSecretAsync("oracle-db");
            OracleDbSecretValue = result;

            var config = new ConfigTest();
            var result2 = config.GetConfigValue("oracle-db");
            OracleDbSecretValue2 = result2;

            var config2 = new ConfigTest();
            var result3 = config.GetConfigValue("password-key");
            OracleDbSecretValue3 = result3;
        }

        public IActionResult OnGetJson()
        {
            var dataList = new[]
            {
                new { Id = 1, Name = "Razor" },
                new { Id = 2, Name = "Pages" },
                new { Id = 3, Name = "API" }
            };
            return new JsonResult(dataList);
        }
    }
}
