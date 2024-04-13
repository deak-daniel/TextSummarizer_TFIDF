using System.Diagnostics;

namespace TextSummarizer
{
    public class Summarizer
    {
        #region Properties
        private List<TF_IDF_Data> TF_IDF;
        private Dictionary<string, int> Word2idx;
        private Dictionary<int, int> TermFrequencyDict;
        private List<List<int>> DataAsIndex;
        private List<string> Data;
        private List<string> RawData;
        private PorterStemmer Stemmer;
        #endregion

        #region Properties
        public List<TF_IDF_Data> tf_idf
        {
            get => this.TF_IDF;
        }
        #endregion

        #region Contructor
        private Summarizer()
        {
            RawData = new List<string>();
            Data = new List<string>();
            TermFrequencyDict = new Dictionary<int, int>();
            Stemmer = new PorterStemmer();
            TF_IDF = new List<TF_IDF_Data>();
            DataAsIndex = new List<List<int>>();
            Word2idx = new Dictionary<string, int>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputData">Input data.</param>
        public Summarizer(string inputData) : this()
        {
            ExtractData(inputData);
            InitWordToIndex();
            ConvertDataToIndex();
            ComputeTfIdf();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Produces the summary in a text format.
        /// </summary>
        /// <returns>The summary in 5 sentences.</returns>
        public string GetSummary() => string.Join(". ", TF_IDF.OrderByDescending(x => x.Avg).Take(5).Select(x => x.RawData));
        #endregion // Public methods

        #region Private methods
        /// <summary>
        /// This method extracts certain data from the input file (it is hardcoded, and can only be used with one data source).
        /// </summary>
        /// <param name="inputfile">Input file path.</param>
        private void ExtractData(string inputfile)
        {
            foreach (string item in File.ReadAllLines(inputfile).Skip(1))
            {
                RawData.Add(item);
                string words = string.Empty;
                string[] helper = item.Split('.',' ');
                if (helper.Length > 2)
                {
                    for (int i = 0; i < helper.Length; i++)
                    {
                        words += Stemmer.StemWord(helper[i]) + " ";
                    }
                    Data.Add(words);
                }

            }
            Console.WriteLine("Extract Done!");
        }
        /// <summary>
        /// Computing TF-IDF (Term frequency - inverse document frequency
        /// </summary>
        private void ComputeTfIdf()
        {
            for (int i = 0; i < DataAsIndex.Count; i++)
            {
                List<int> tfidf = new List<int>();
                for (int j = 0; j < DataAsIndex[i].Count; j++)
                {
                    tfidf.Add(ComputeTermFrequency(DataAsIndex[i], DataAsIndex[i][j]) * (int)(Math.Log(Data.Count / ComputeDocumentFrequency(DataAsIndex[i][j]))));
                }
                TF_IDF.Add(new TF_IDF_Data(RawData[i], DataAsIndex[i], tfidf));
                Debug.WriteLine(i);
            }
        }
        /// <summary>
        /// Initializes Word-To-Index mapping.
        /// </summary>
        private void InitWordToIndex()
        {
            int index = 1;
            for (int i = 0; i < Data.Count; i++)
            {
                string[] helper = Data[i].Split(" ");
                for (int j = 0; j < helper.Length; j++)
                {
                    string cleanedString = helper[j].CleanString();
                    if (!Word2idx.ContainsKey(cleanedString))
                    {
                        Word2idx.Add(cleanedString, index++);
                    }
                }
            }
            Console.WriteLine("Word To Index mapping Initialized!");
        }
        /// <summary>
        /// Converts the data to each word's corresponding index.
        /// </summary>
        private void ConvertDataToIndex()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                List<int> item = new List<int>();
                string[] helper = Data[i].Split(" ");

                for (int j = 0; j < helper.Length; j++)
                {
                    string cleanedString = helper[j].CleanString();
                    if (cleanedString is not "")
                    {
                        item.Add(Word2idx[cleanedString]);
                    }
                }

                DataAsIndex.Add(item);
                Debug.WriteLine(i);

            }
            Console.WriteLine("Data as Index done!");
        }
        /// <summary>
        /// Computes document frequency of a word.
        /// </summary>
        /// <param name="word">the word which we are searching for</param>
        /// <returns>a document frequency foor a certain word</returns>
        private int ComputeDocumentFrequency(int word)
        {
            if (TermFrequencyDict.ContainsKey(word))
            {
                return TermFrequencyDict[word];
            }
            else
            {
                int counter = 0;
                for (int i = 0; i < DataAsIndex.Count; i++)
                {
                    if (DataAsIndex[i].Contains(word))
                    {
                        counter++;
                    }
                }
                TermFrequencyDict.Add(word, counter);

                return counter;
            }
        }
        /// <summary>
        /// How often does a certain word appears in one given document.
        /// </summary>
        /// <param name="document">The given document.</param>
        /// <param name="word">The given word.</param>
        /// <returns>The number of times a word appears in the document</returns>
        private int ComputeTermFrequency(List<int> document, int word)
        {
            int counter = 0;
            for (int i = 0; i < document.Count; i++)
            {
                if (document[i] == word)
                {
                    counter++;
                }
            }
            return counter;
        }
        #endregion // Private methods
    }
}
