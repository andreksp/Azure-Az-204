using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Azure_Az_204.AzureKeyVault
{
    public class AzureKeyVault
    {
        public static void CallAzureKeyValt()
        {
            // Provide the vault URL
            string whizlab_vault = "https://whizlabvault6000.vault.azure.net/";
            // Create a connection to the vault
            var w_client = new SecretClient(vaultUri: new Uri(whizlab_vault), credential: new DefaultAzureCredential());

            // Get the value of the secret
            KeyVaultSecret w_secret = w_client.GetSecret("whizlabpassword");
            Console.WriteLine(w_secret.Value);
            Console.ReadKey();

        }
    }
}
