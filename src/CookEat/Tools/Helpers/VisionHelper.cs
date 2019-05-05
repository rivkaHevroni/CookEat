using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using Grpc.Core;

namespace CookEat
{
    

    public sealed class VisionHelper
    {
        private readonly ImageAnnotatorClient _client;

        public VisionHelper()
        {
            var credentials = GoogleCredential.FromFile(@"C:\CookEat-google-api.json");
            var channel = new Channel(
                ImageAnnotatorClient.DefaultEndpoint.Host,
                ImageAnnotatorClient.DefaultEndpoint.Port,
                credentials.ToChannelCredentials());

            _client = ImageAnnotatorClient.Create(channel);
        }

        public void CreateQueryFromImage() //byte[] imageBytes , return string
        {
            //var image = Image.FromBytes(imageBytes);
            var image = Image.FromUri("https://www.skinnytaste.com/wp-content/uploads/2011/07/Easiest-Pasta-and-Broccoli-Recipe-550x367.jpg");
            var annotation = _client.DetectWebInformation(image);
            var bestLabels = annotation.BestGuessLabels;

            //create json with the byts
            //sent the json to vision API and get json with Query in english
            // sent the json to TranslateHelper and get query in Hebrew
            //create QueryOfRecipe from the json

            //return QueryOfRecipe;
        }
    }
}