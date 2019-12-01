﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitoBot
{
    public class AylienSentimentFetch : ISentimentFetch
    {
        public string getSentimentScore(string text)
        {
            var client = new RestClient("https://api.aylien.com/api/v1/sentiment?mode=tweet&text=" + text);
            var request = new RestRequest(Method.GET);
            addHeaders(request);
            IRestResponse response = client.Execute(request);
            SentimentResponse reply = SimpleJson.DeserializeObject<SentimentResponse>(response.Content);
            return reply.polarity;
        }

        public void addHeaders(RestRequest request) {
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-AYLIEN-TextAPI-Application-ID", "9915cb86");
            request.AddHeader("X-AYLIEN-TextAPI-Application-Key", "b0cdea5fc81ec683cb681eec282fcb12");
        }
        public class SentimentResponse
        {
            public string polarity { get; set; }
            public string subjectivity { get; set; }
            public string text { get; set; }
            public float polarity_confidence { get; set; }
            public float subjectivity_confidence { get; set; }
        }

    }
}
