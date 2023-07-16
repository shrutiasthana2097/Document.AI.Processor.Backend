using Azure;
using Azure.AI.TextAnalytics;

namespace Document.Processor.AI.Services
{
    public class TextAnalyticsService : ITextAnalyticsService
    {
        private readonly TextAnalyticsClient _client;
        private readonly IConfiguration _config;
        public TextAnalyticsService(IConfiguration config)
        {
            _config = config;
            Uri endpoint = new(_config["AzureAI:APIUrl"]);
            AzureKeyCredential credential = new(_config["AzureAI:APIkey"]);
            _client = new(endpoint, credential);
        }
        public KeyPhraseCollection GetKeyPhrases(string text)
        {
            try
            {
                Response<KeyPhraseCollection> response = _client.ExtractKeyPhrases(text);
                KeyPhraseCollection keyPhrases = response.Value;
                return keyPhrases;
            }
            catch (RequestFailedException exception)
            {
                Console.WriteLine($"Error Code: {exception.ErrorCode}");
                Console.WriteLine($"Message: {exception.Message}");
            }
            return null;
        }
        public CategorizedEntityCollection GetNamedEntities(string text)
        {
            try
            {
                Response<CategorizedEntityCollection> response 
                    = _client.RecognizeEntities(text);

                CategorizedEntityCollection entitiesInDocument = response.Value;
                return entitiesInDocument;
            }
            catch (RequestFailedException exception)
            {
                Console.WriteLine($"Error Code: {exception.ErrorCode}");
                Console.WriteLine($"Message: {exception.Message}");
            }
            return null;
        }
        public DocumentSentiment AnalyzeSentiments(string text)
        {
            try
            {
                Response<DocumentSentiment> response = _client.AnalyzeSentiment(text);
                DocumentSentiment docSentiment = response.Value;
                return docSentiment;
            }
            catch (RequestFailedException exception)
            {
                Console.WriteLine($"Error Code: {exception.ErrorCode}");
                Console.WriteLine($"Message: {exception.Message}");
            }
            return null;
        }
        public PiiEntityCollection RecognizePIIEntities(string text)
        {
            try
            {
                Response<PiiEntityCollection> response = _client.RecognizePiiEntities(text);
                PiiEntityCollection entities = response.Value;
                return entities;
            }
            catch (RequestFailedException exception)
            {
                Console.WriteLine($"Error Code: {exception.ErrorCode}");
                Console.WriteLine($"Message: {exception.Message}");
            }
            return null;
        }
        public async Task<List<string>> ExtractSummary(string text)
        {
            List<string> summary = new List<string>();
            List<string> batchedDocuments = new()
            {
               text
            };

            ExtractiveSummarizeOperation operation = 
                _client.ExtractiveSummarize(WaitUntil.Completed, batchedDocuments);

            await foreach (ExtractiveSummarizeResultCollection documentsInPage in operation.Value)
            {
                foreach (ExtractiveSummarizeResult documentResult in documentsInPage)
                {
                    foreach (ExtractiveSummarySentence sentence in documentResult.Sentences)
                    {
                        summary.Add(sentence.Text);
                    }
                }
            }
            return summary;
        }
        public async Task<List<HealthcareEntity>> GetHealthcareEntities(string text)
        {
            List<HealthcareEntity> result = new List<HealthcareEntity>();
            List<string> batchedDocuments = new()
            {
                text
            };

            // Perform the text analysis operation.
            AnalyzeHealthcareEntitiesOperation operation = 
                await _client.AnalyzeHealthcareEntitiesAsync(WaitUntil.Completed, batchedDocuments);
            // View the operation results.
            await foreach (AnalyzeHealthcareEntitiesResultCollection documentsInPage in operation.Value)
            {
              
                foreach (AnalyzeHealthcareEntitiesResult documentResult in documentsInPage)
                {

                    // View the healthcare entities that were recognized.
                    foreach (HealthcareEntity entity in documentResult.Entities)
                    {
                        result.Add(entity);
                        
                    }

                    
                }
               
            }
            return result;
        }
    }
}
