namespace Document.Processor.AI.Services
{
    public interface IComputerVision
    {
        List<string> OCRText(MemoryStream ms);
    }
}
