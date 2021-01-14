using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using TestLibrary;
using System;
using UnityEngine.UI;
using Assets;

public class CallService : MonoBehaviour
{

   
    public Text guiText;
    string text = "Wait for call";
    BaseService service;

    // Start is called before the first frame update
    void Start()
    {
        service = new BaseService();
        service.UserLoaded += Service_UserLoaded;
        service.UserNoEnumLoaded += Service_UserNoEnumLoaded;
    }

    private void Service_UserNoEnumLoaded(object sender, UserNoEnum result)
    {
        text = $"{result.Name} {result.Age}";
    }

    private void Service_UserLoaded(object sender, User result)
    {
        text = $"{result.Name} {result.Age} {result.Status}";
    }

    public async void GetUser()
    {
        await service.GetUser();
    }

    public async void GetUserNoEnum()
    {
        await service.GetUserNoEnum();
    }

    // Update is called once per frame
    void Update()
    {
        if (guiText?.text != text)
        {
            guiText.text = text;
        }
    }
}
