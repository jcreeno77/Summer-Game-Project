using Newtonsoft.Json.Linq;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.Json;
using System.Net;

public class OpenAITesting : MonoBehaviour
{
    public float movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        test();
    }

    public class Person
    {
        public string Name;
        public int Age;
        public string Height;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            test();
        }
    }

    async void test()
    {
        var api = new OpenAIClient();

        var model = Model.GPT3_5_Turbo;
        Debug.Log(model.ToString());

        //var result = await api.CompletionsEndpoint.CreateCompletionAsync("One Two Three One Two", temperature: 0.1, model: Model.GPT3_5_Turbo_16K);

        var chatPrompts = new List<Message>
        {
            new Message(Role.System,"You generate a JSON for the user. You do not add extra commentary or speech. You only give the JSON.\n" +
            "Never respond conversationally with something like: 'Sure! Here's a JSON for a character with a random Russian name:'\n" +
            "An example is this: \n" +
            "User: Hey give me a JSON of a character named John. \n" +
            "Response: {\"Name\":\"John\", \"Age\":\"30\", \"Height\": \"5.8\"}\n" +
            "You will ONLY include Name, Age, and Height. The age range should be between 5-60"),
            new Message(Role.User, "Hey give me a JSON for a character with a random russian name.")
        };

        var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);
        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest);                                  

        Debug.Log(result.ToString());
        
        Person person = JsonUtility.FromJson<Person>(result.ToString());
        Debug.Log(person.Age);
        Debug.Log(person.Name);
        movementSpeed = person.Age;

    }
}
