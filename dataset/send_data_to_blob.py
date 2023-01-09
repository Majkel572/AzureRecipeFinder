import os
from azure.storage.blob import BlobServiceClient
from azure.keyvault.secrets import SecretClient
from azure.identity import DefaultAzureCredential

keyVaultName = 'keyvolt7712'
keyVaultURL = f"https://{keyVaultName}.vault.azure.net"

credential = DefaultAzureCredential()
client = SecretClient(vault_url=keyVaultURL, credential=credential)

secretName = 'blobCS'

retrieved_secret = client.get_secret(secretName)

connection_string = retrieved_secret.value
blob_service_client = BlobServiceClient.from_connection_string(connection_string)
container_client = blob_service_client.get_container_client("recipestorage")
folder = './send_to_blob/'
recipes_list = os.listdir(folder)
for recipe in recipes_list:
    blob_client = blob_service_client.get_blob_client(container='recipestorage', blob=f"{recipe}")
    with open(f"./send_to_blob/{recipe}", "rb") as data:
        blob_client.upload_blob(data)