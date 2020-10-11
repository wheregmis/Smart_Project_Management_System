using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMS.sentimentanalysis
{
    class SentimentAnalysis
    {
        private static readonly string BaseModelsRelativePath = @"../../MLModels";
        private static readonly string ModelRelativePath = $"{BaseModelsRelativePath}/SentimentModel.zip";

        private static string ModelPath = GetAbsolutePath(ModelRelativePath);
        string sentimentToxicProb;
        string sentimentToxic;
        String sentimentText;
        public SentimentAnalysis(String sentiment)
        {
            this.sentimentText = sentiment;
            var mlContext = new MLContext(seed: 1);
            TestSinglePrediction(mlContext, sentiment);

        }

        private void TestSinglePrediction(MLContext mlContext, string sentiment)
        {
            SentimentIssue sampleStatement = new SentimentIssue { Text = sentiment.ToString() };

            ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predEngine = mlContext.Model.CreatePredictionEngine<SentimentIssue, SentimentPrediction>(trainedModel);

            //Score
            var resultprediction = predEngine.Predict(sampleStatement);

            Console.WriteLine($"=============== Single Prediction  ===============");
            Console.WriteLine($"Text: {sampleStatement.Text} | Prediction: {(Convert.ToBoolean(resultprediction.Prediction) ? "Toxic" : "Non Toxic")} sentiment | Probability of being toxic: {resultprediction.Probability} ");
            Console.WriteLine($"==================================================");

          //  MessageBox.Show($"Text: {sampleStatement.Text} | Prediction: {(Convert.ToBoolean(resultprediction.Prediction) ? "Toxic" : "Non Toxic")} sentiment | Probability of being toxic: {resultprediction.Probability} ");

            this.sentimentToxic = $"{(Convert.ToBoolean(resultprediction.Prediction) ? "Toxic" : "Non Toxic")}";
            this.sentimentToxicProb = $"{resultprediction.Probability}";

            

           
          //  MessageBox.Show(sentimentToxicProb);
        }

        public Double getProbability() {

            return Convert.ToDouble( this.sentimentToxicProb);
        }

        public string getSentiment()
        {
            return this.sentimentToxic;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(MainWindow).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = System.IO.Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

    }

}
