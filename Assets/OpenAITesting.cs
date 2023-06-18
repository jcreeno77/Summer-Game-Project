using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAITesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void test()
    {
        var api = new OpenAIClient();

        var model = Model.GPT3_5_Turbo;
        Debug.Log(model.ToString());

        //var result = await api.CompletionsEndpoint.CreateCompletionAsync("One Two Three One Two", temperature: 0.1, model: Model.GPT3_5_Turbo_16K);

        var chatPrompts = new List<Message>
        {
            new Message(Role.System,"You generate a JSON for the user. You do not add extra commentary or speech. You only give the JSON." +
            "And example is this: \n" +
            "User: Hey give me a JSON of a character named John. \n" +
            "Response: {'Name':'John', 'Age':'30', 'Height': '5.8'}"),
            new Message(Role.User, "Hey give me a JSON for a character named Margaret.")
        };

        var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);
        var result = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
        Debug.Log(result);

    }
}
