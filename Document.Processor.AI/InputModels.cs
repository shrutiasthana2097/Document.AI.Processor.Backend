namespace Document.Processor.AI
{
    public class InputModels
    {

    }
    public class GetOCRInput
    {
        public IFormFile InputText { get; set; }
    }
    public class GetNamedEntities
    {
        public string InputText { get; set; }
    }
    public class GetKeyPhrases
    {
        public string InputText { get; set; }
    }
    public class AnalyzeSentiments
    {
        public string InputText { get; set; }
    }
}
