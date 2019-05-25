using NUnit.Framework;
using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ItemApiTest()
        {
            var testResult = new StringBuilder();
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Assert.Fail(disco.Error);
                return;
            }

            // request token
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "client",
            //    ClientSecret = "secret",

            //    Scope = "api1"
            //});


            // request token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"

            });

            if (tokenResponse.IsError)
            {
                Assert.Fail(tokenResponse.Error);
                return;
            }




            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            testResult.AppendLine("-----------Token------------");
            testResult.AppendLine(tokenResponse.AccessToken);



            var response = await apiClient.GetAsync("http://localhost:5001/api/items");
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail(response.StatusCode.ToString());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                testResult.AppendLine("-----------Items Result------------");
                testResult.AppendLine(content);
            }

            var stringContent = new StringContent("", UnicodeEncoding.UTF8, "application/json");

            var responseBuy = await apiClient.PostAsync("http://localhost:5001/api/items/5/buy", stringContent);
            if (!responseBuy.IsSuccessStatusCode)
            {
                var error = await responseBuy.Content.ReadAsStringAsync();
                Assert.Fail(responseBuy.StatusCode.ToString() +"" + error);
            }
            else
            {
                var content = await responseBuy.Content.ReadAsStringAsync();
                testResult.AppendLine("-----------Update Result------------");
                testResult.AppendLine(content);
            }

            Assert.Pass(testResult.ToString());
        }
    }
}