using System;
using System.Collections.Generic;
using OpenAI;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
    public class ChatGPTTest : MonoBehaviour
    {
        
        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Act as a psychologist in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";

        
        private void Start()
        {
            Debug.Log("Sending request");
            //SendRequest();
            SendReply();
        }
        
        private void SendRequest()
        {
            //var openai = new OpenAIApi("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");
            var request = new CreateCompletionRequest{
                Model="text-davinci-003",
                Prompt="Say this is a test and dance your favourite dance",
            };
            //var response = await openai.CreateCompletion(request);
            //Debug.Log(response.Model);
        }
        
        private async void SendReply()
        {
            //var openai = new OpenAIApi("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");
                
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = "Say this is a test and dance your favourite dance"
            };
            

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + "Say this is a test and dance your favourite dance"; 
            
            messages.Add(newMessage);

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                Debug.Log(message.Content);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }
        }
    }
}