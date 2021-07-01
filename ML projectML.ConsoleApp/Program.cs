using Microsoft.ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.ML.DataOperationsCatalog;



namespace ML_projectML.ConsoleApp
{
    class Program
    {   //Full path to ML model
        private static readonly string DataPath = @"C:\Users\Bcrac\Desktop\ML project\ML projectML.ConsoleApp\Files\wikiDetoxAnnotated40kRows.tsv";

        static void Main(string[] args)
        {
            //Full path to JSON file
            var path = @"C:\Users\Bcrac\Desktop\ML project\ML projectML.ConsoleApp\Files\data.json";
            string jsonfile = File.ReadAllText(path);


            var c = JsonConvert.DeserializeObject<List<string[]>>(jsonfile);

            //Debug Show elements of list
            //foreach (var item in c)
            // {
            // Console.WriteLine("{0}", item);
            //}

            // Create MLContext to be shared across the model creation workflow objects 
            // Set a random seed for repeatable/deterministic results across multiple trainings.
            var mlContext = new MLContext(seed: 1);

            // STEP 1: Common data loading configuration
            IDataView dataView = mlContext.Data.LoadFromTextFile<SentimentIssue>(DataPath, hasHeader: true);

            TrainTestData trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            IDataView trainingData = trainTestSplit.TrainSet;
            IDataView testData = trainTestSplit.TestSet;

            // STEP 2: Common data process configuration with pipeline data transformations          
            var dataProcessPipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(SentimentIssue.Text));

            // STEP 3: Set the training algorithm, then create and config the modelBuilder                            
            var trainer = mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            // STEP 4: Train the model fitting to the DataSet
            ITransformer trainedModel = trainingPipeline.Fit(trainingData);

            // STEP 5: Evaluate the model and show accuracy stats
            var predictions = trainedModel.Transform(testData);
            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");
            int i = 0;
            float probsum = 0;
            float prob = 0;
            foreach (var item in c)
            {
                //Counter for array elements
                i++;

                string result = string.Join(" ", item);
                var predEngine = mlContext.Model.CreatePredictionEngine<SentimentIssue, SentimentPrediction>(trainedModel);
                SentimentIssue sampleStatement = new SentimentIssue { Text = result };
                var resultprediction = predEngine.Predict(sampleStatement);

                //Debug
                //Console.WriteLine($"Prediction: {(Convert.ToBoolean(resultprediction.Prediction) ? "Toxic" : "Non Toxic")} sentiment");
                //Console.WriteLine(resultprediction.Probability);
                probsum = resultprediction.Probability + probsum;
            }
            prob = probsum / i;

            Console.WriteLine();

            Console.WriteLine($"Number of comments: " + i);
            Console.WriteLine($"Probability:  " + prob);
            Console.WriteLine($" ");
            Console.WriteLine($" ");
            Console.WriteLine($" ");
            Console.WriteLine($"================End of Process.Hit any key to exit==================================");
            Console.ReadLine();

        }

    }
}
