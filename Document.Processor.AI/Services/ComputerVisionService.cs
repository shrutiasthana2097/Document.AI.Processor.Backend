using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace Document.Processor.AI.Services
{
    public class ComputerVisionService:IComputerVision
    {
        private readonly ComputerVisionClient _client;
        private readonly IConfiguration _conf;
        public ComputerVisionService(IConfiguration conf)
        {
            _conf = conf;
            _client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_conf["AzureAI:APIkey"]))
              { Endpoint = _conf["AzureAI:APIUrl"] };
        }
        public List<string> OCRText(MemoryStream ms)
        {
            List<string> strArray=new List<string>();

            var textHeaders = _client.ReadInStreamAsync(ms).Result;
            string operationLocation = textHeaders.OperationLocation;
            Thread.Sleep(2000);

            const int numberOfCharsInOperationId = 36;
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);
           
            // Extract the text
            ReadOperationResult results;
            do
            {
                results =_client.GetReadResultAsync(Guid.Parse(operationId)).Result;
            }
            while ((results.Status == OperationStatusCodes.Running ||
                results.Status == OperationStatusCodes.NotStarted));

            var textUrlFileResults = results.AnalyzeResult.ReadResults;
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    strArray.Add(line.Text);
                }
            }
            return strArray;
        }
    }
}
