using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSummarizer
{
    public class TF_IDF_Data
    {
        #region Properties
        /// <summary>
        /// The property representing a certain value's raw data.
        /// </summary>
        public string RawData { get; private set; }
        /// <summary>
        /// The dataVector, which means, the raw data's converted version.
        /// </summary>
        public List<int> DataVector { get; private set; }
        /// <summary>
        /// The TF-IDF vector.
        /// </summary>
        public List<int> TF_IDF { get; private set; }
        /// <summary>
        /// The average of the non-zero values in the TF-IDF.
        /// </summary>
        public double Avg { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rawData">The raw string data.</param>
        public TF_IDF_Data(string rawData)
        {
            this.RawData = rawData;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataVector">The data converted into a vector format</param>
        /// <param name="tfidf">The TF-IDF (Term-Frequency Inverse-Docuemtn-Frequency) of the Data</param>
        public TF_IDF_Data(string rawData, List<int> dataVector, List<int> tfidf) : this(rawData)
        {
            this.DataVector = dataVector;
            this.TF_IDF = tfidf;
            CalculateAverage();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Calculates the average of the non-zero values in the TF-IDF.
        /// </summary>
        private void CalculateAverage()
        {
            int sum = 0;
            int counter = 0;
            for (int i = 0; i < TF_IDF.Count; i++)
            {
                if (TF_IDF[i] is not 0)
                {
                    counter++;
                    sum += TF_IDF[i];
                }
            }
            this.Avg = (double)sum / counter;
        }
        #endregion
    }
}
