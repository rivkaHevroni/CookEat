using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Google.Cloud.Translation.V2;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookEat
{
    public sealed class GoogleApiHelper
    {
        private readonly ImageAnnotatorClient _visionClient;
        private readonly TranslationClient _translateClient;

        public GoogleApiHelper()
        {
            var credentials = GoogleCredential.FromFile(@"C:\CookEat-google-api.json");
            var visionChannel = new Channel(
                ImageAnnotatorClient.DefaultEndpoint.Host,
                ImageAnnotatorClient.DefaultEndpoint.Port,
                credentials.ToChannelCredentials());

            _visionClient = ImageAnnotatorClient.Create(visionChannel);
            _translateClient = TranslationClient.Create(credentials);
        }

        public async Task<string> GetQueryFromImage(string imageBase64String)
        {
            var englishQuery = await GetImageBestGuessLabelsAsync(imageBase64String);
            return await TranslateToHebrewAsync(englishQuery);
        }

        private async Task<string> TranslateToHebrewAsync(string source)
        {
            return (await _translateClient.
                TranslateTextAsync(source, "he")).
                TranslatedText;
        }

        private async Task<string> GetImageBestGuessLabelsAsync(string imageBase64String)
        {
			byte[] imageBytes = Convert.FromBase64String(imageBase64String);
			var image = Image.FromBytes(imageBytes);
            var annotation = await _visionClient.DetectWebInformationAsync(image);
			
            return annotation.
                BestGuessLabels.
                ToString();
        }
    }
}