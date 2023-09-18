az ad sp create-for-rbac -n whizlabapp --skip-assignment

Define the following environment variables first

AZURE_CLIENT_ID="38ab1ada-1c41-4bc6-af9e-440db377a2d8"
AZURE_CLIENT_SECRET="cae68966-471e-4714-bc0d-3168cb3d3c3a"
AZURE_TENANT_ID="97ab3455-f094-48ff-b855-10d642093cf2"


az keyvault set-policy --name whizlabvault6000 --spn "38ab1ada-1c41-4bc6-af9e-440db377a2d8" --secret-permissions backup delete get list set


C# Nugget Packages
Azure.Security.KeyVault.Secrets
Azure.Identity
Azure.Core