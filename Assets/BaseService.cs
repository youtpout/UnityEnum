using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;
using UnityEngine;

namespace Assets
{
    public class BaseService
    {
        string url = "http://192.168.1.110:55555";
        HubConnection connection;

        public event EventHandler<User> UserLoaded;
        public event EventHandler<UserNoEnum> UserNoEnumLoaded;

        public BaseService()
        {
            connection = new HubConnectionBuilder()
               .WithUrl($"{url}/TestHub")
               .WithAutomaticReconnect()
               .Build();

            Start();
        }

        async Task Start()
        {
            await connection.StartAsync();

            connection.On<User>("UserLoaded", (result) =>
            {
                Debug.Log("User");
                if (UserLoaded != null)
                {
                    UserLoaded(this, result);
                }
                //text = $"{result.Name} {result.Age} {result.Status}";
            });

            connection.On<UserNoEnum>("UserNoEnumLoaded", (result) =>
            {
                Debug.Log("UserNoEnum");
                if (UserNoEnumLoaded != null)
                {
                    UserNoEnumLoaded(this, result);
                }
                // 
            });
        }

        public async Task GetUser()
        {
            await connection.InvokeAsync("Get");
        }

        public async Task GetUserNoEnum()
        {
            await connection.InvokeAsync("GetNoEnum");
        }

    }
}
