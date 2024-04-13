using TextSummarizer;

namespace TextSummarizer
{
    internal class Program
    {
        // if-idf
        //  Text frequency - inverse document frequency
        // means: tf: Number of times a word appears in a certain document
        //        idf: Log (Number of docuemnts / the number of documents that contain the word)
        static void Main(string[] args)
        {
            Summarizer summarizer = new Summarizer("287.txt");
            Console.WriteLine($"{summarizer.GetSummary()}");
        }
    }
}
