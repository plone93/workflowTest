using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace razorWebApplication1.Utils
{
    public class KeyVaultTest
    {
        private readonly SecretClient _client;


        public KeyVaultTest()
        {
            var keyVaultName = "my-keyvault-123";
            if (string.IsNullOrEmpty(keyVaultName))
                throw new Exception("keyVaultNameが未設定");

            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            //　デフォルトの認証情報を使用する場合はこちらを使用（環境変数から取得、TENANT_ID、CLIENT_ID、CLIENT_SECRET）
            _client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            //　ブラウザでの認証を行う場合はこちらを使用
            //_client = new SecretClient(new Uri(kvUri), new InteractiveBrowserCredential());

        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            Console.WriteLine($"🔍 Retrieving your secret '{secretName}'...");
            var secret = await _client.GetSecretAsync(secretName);
            Console.WriteLine($"✅ Your secret is '{secret.Value.Value}'.");
            return secret.Value.Value;
        }

        public async Task PurgeSecretAsync(string secretName)
        {
            Console.WriteLine($"🧹 Purging your secret '{secretName}'...");
            await _client.PurgeDeletedSecretAsync(secretName);
            Console.WriteLine("✅ Purge done.");
        }
    }
}
