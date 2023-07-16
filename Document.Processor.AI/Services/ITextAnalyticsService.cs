using Azure.AI.TextAnalytics;

namespace Document.Processor.AI.Services
{
    public interface ITextAnalyticsService
    {
        CategorizedEntityCollection GetNamedEntities(string text);
        KeyPhraseCollection GetKeyPhrases(string text);
        DocumentSentiment AnalyzeSentiments(string text);
        PiiEntityCollection RecognizePIIEntities(string text);
        Task<List<string>> ExtractSummary(string text);

        Task<List<HealthcareEntity>> GetHealthcareEntities(string text);
    }
}
