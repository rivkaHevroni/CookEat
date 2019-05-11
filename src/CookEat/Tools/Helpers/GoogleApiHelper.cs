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

        public async Task<string> GetQueryFromImage(byte[] imageBytes)
        {
            var englishQuery = await GetImageBestGuessLabelsAsync(imageBytes);
            return await TranslateToHebrewAsync(englishQuery);
        }

        private async Task<string> TranslateToHebrewAsync(string source)
        {
            return (await _translateClient.
                TranslateTextAsync(source, "he")).
                TranslatedText;
        }

        private async Task<string> GetImageBestGuessLabelsAsync(byte[] imageBytes)
        {
            var image = Image.FromBytes(imageBytes);
            var annotation = await _visionClient.DetectWebInformationAsync(image);

            return annotation.
                BestGuessLabels.
                ToString();
        }
    }
}