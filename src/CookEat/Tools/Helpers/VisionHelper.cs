using Google.Cloud.Vision.V1;


namespace CookEat
{
    public class VisionHelper
    {
		public void CreateQueryFromImage() //byte[] imageBytes , return string
		{
			string QueryOfRecipe = null;
			//var image = Image.FromBytes(imageBytes);
			var image = Image.FromUri("http://f.nanafiles.co.il/upload/mediastock/img/1789/0/33/33248.jpg");
			var client = ImageAnnotatorClient.Create();
			WebDetection annotation = client.DetectWebInformation(image);
			var bestLabels = annotation.BestGuessLabels;

			//create json with the byts
			//sent the json to vision API and get json with Query in english
			// sent the json to TranslateHelper and get query in Hebrew
			//create QueryOfRecipe from the json

			//return QueryOfRecipe;
        }
    }
}
