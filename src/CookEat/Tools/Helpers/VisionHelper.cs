using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Vision.V1;

namespace CookEat
{
    public class VisionHelper
    {
        private readonly ImageAnnotatorClient _client;

        public VisionHelper()
        {
            _client = ImageAnnotatorClient.Create();
        }

        public async Task<List<string>> GetImageBestSearchLabelsAsync(byte[] imageBytes)
        {
            var image = Image.FromBytes(imageBytes);

            var response = await _client.DetectWebInformationAsync(image);

            return response.
                BestGuessLabels.
                Where(label => label.LanguageCode == "en").
                Select(label => label.Label).
                ToList();
        }
    }
}
