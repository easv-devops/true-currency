using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

string secretName = "currency-conn";
string keyVaultName = "currency";
var kvUri = "https://currency.vault.azure.net/";
SecretClientOptions options = new SecretClientOptions()
{
    Retry =
    {
        Delay= TimeSpan.FromSeconds(2),
        MaxDelay = TimeSpan.FromSeconds(16),
        MaxRetries = 5,
        Mode = RetryMode.Exponential
    }
};

var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(),options);

Console.WriteLine(client.GetSecret(secretName).Value);

