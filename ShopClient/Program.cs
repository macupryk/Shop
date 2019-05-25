// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
            Console.ReadKey();
        }
        public static async Task MainAsync(string[] args)
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine("------------Token Error---------------------");
                Console.WriteLine(disco.Error);
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
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine("------------Token Response---------------------");
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:5001/api/items");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("------------Items api error---------------------");
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(error);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("------------Items api response----------------------");
                Console.WriteLine(JObject.Parse(content));
            }

            var stringContent = new StringContent("", UnicodeEncoding.UTF8, "application/json");

            var responseBuy = await apiClient.PostAsync("http://localhost:5001/api/items/5/buy", stringContent);
            if (!responseBuy.IsSuccessStatusCode)
            {
                var error= await responseBuy.Content.ReadAsStringAsync();
                Console.WriteLine("------------Buy api error---------------------");
                Console.WriteLine(responseBuy.StatusCode);
                Console.WriteLine(error);

            }
            else
            {
                var content = await responseBuy.Content.ReadAsStringAsync();
                Console.WriteLine("------------Buy api response---------------------");

                Console.WriteLine(JObject.Parse(content));

            }
        }
    }
}