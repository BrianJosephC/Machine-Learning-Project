using Microsoft.ML.Data;

namespace ML_projectML.ConsoleApp
{
    public class SentimentIssue
    {
        [LoadColumn(0)]
        public bool Label { get; set; }
        //load col 1
        [LoadColumn(2)]
        public string Text { get; set; }
    }
}
