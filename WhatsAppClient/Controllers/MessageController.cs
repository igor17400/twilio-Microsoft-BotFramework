using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;
using WhatsAppClient.Models;

namespace WhatsAppClient.Controllers
{
    public class MessageController : TwilioController
    {
        string qnaMakerName = "chitchat_general";
        string knowledgeBaseId = "7ee6f598-6912-4673-877b-9bf600fe468c";
        string authorization = "EndpointKey 65163eca-d0b6-4ec3-b225-99ea58f6678a";

        //public TwiMLResult Index(SmsRequest incomingMessage)
        //{
        //    var messagingResponse = new MessagingResponse();
        //    string answer = GetAnswerFromText(incomingMessage.Body);
        //    Console.WriteLine("---------------> " + answer);
        //    messagingResponse.Message(answer);

        //    return TwiML(messagingResponse);

        //    return TwiML(response);
        //}

        public ActionResult SendMessage()
        {
            var accountSid = "AC81de9e73fa377c2926c72d6b79ed3cef";
            var authToken = "11170fa218441b4ec9e0a9d9dcd3160a";
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hello there!",
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                to: new Twilio.Types.PhoneNumber("whatsapp:+556196995651")
            );

            return Content(message.Sid);
        }

        public ActionResult ReceiveMessage(SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            var mss = messagingResponse.Message(
                body: "Hello there!"
            );

            return TwiML(mss);
        }

        private string GetAnswerFromText(string question)
        {
            string receivedQuestion = string.Format("{{\"question\":\"{0}\"}}", question);

            var client = new RestClient($"https://{qnaMakerName}.azurewebsites.net/qnamaker/knowledgebases/{knowledgeBaseId}/generateAnswer");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", authorization);
            request.AddParameter("undefined", receivedQuestion, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var createdAnswer = JsonConvert.DeserializeObject<Answer>(response.Content).answers[0].answer;
            Console.WriteLine("---------------> " + createdAnswer);
            return createdAnswer;
        }
    }
}