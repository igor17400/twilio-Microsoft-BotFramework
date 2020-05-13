using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsAppClient.Models
{
    public class Answer
    {
        public QnAReceivedAnswer[] answers { get; set; }
    }

    public class QnAReceivedAnswer
    {
        public string[] questions { get; set; }
        public string answer { get; set; }
        public double score { get; set; }
        public int id { get; set; }
        public string source { get; set; }
        public object[] metadata { get; set; }
    }
}