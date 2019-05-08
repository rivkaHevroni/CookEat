using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Google.Cloud.Translation.V2;
using Grpc.Auth;
using Grpc.Core;
using System;

namespace CookEat
{
    public sealed class VisionAndTranslateHelper
    {
        private readonly ImageAnnotatorClient _visionClient;
        private readonly TranslationClient _translateClient;

        public VisionAndTranslateHelper()
        {
            var credentials = GoogleCredential.FromFile(@"C:\CookEat-google-api.json");
            var visionChannel = new Channel(
                ImageAnnotatorClient.DefaultEndpoint.Host,
                ImageAnnotatorClient.DefaultEndpoint.Port,
                credentials.ToChannelCredentials());

            _visionClient = ImageAnnotatorClient.Create(visionChannel);
            _translateClient = TranslationClient.Create(credentials);
        }

        public string CreateQueryFromImage(byte[] imageBytes) 
        {
            var image = Image.FromBytes(imageBytes);
            //var image = Image.FromUri("https://www.ynet.co.il/PicServer2/24012010/2826420/6_wa.jpg");
            var annotation = _visionClient.DetectWebInformation(image);
            var bestLabels = annotation.BestGuessLabels;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var response = _translateClient.TranslateText(bestLabels.ToString(), "he");

            return response.TranslatedText;
        }
    }
}