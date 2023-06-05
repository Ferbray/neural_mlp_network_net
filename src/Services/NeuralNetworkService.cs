using src.Common;
using System;
using System.Linq;

namespace src.Services
{
    public class NeuralNetworkService
    {
        private readonly int _numInput;
        private readonly int _numHidden;
        private double[][] _ihWeight;
        private readonly double[][] _ihWeightDelta;
        private double[][] _hoWeight;
        private readonly double[][] _hoWeightDelta;

        private readonly Random random = new();

        public NeuralNetworkService(int numInput, int numHidden)
        {
            _numInput = numInput;
            _numHidden = numHidden;

            _ihWeight = Helper.MakeMatrix(numInput, numHidden);
            _hoWeight = Helper.MakeMatrix(numHidden, 1);
            _ihWeightDelta = Helper.MakeMatrix(numInput, numHidden);
            _hoWeightDelta = Helper.MakeMatrix(numHidden, 1);
        }

        public void InitializeWeights()
        {
            for (int i = 0; i < _numInput; ++i)
                for (int j = 0; j < _numHidden; ++j) _ihWeight[i][j] = random.NextDouble();

            for (int i = 0; i < _numHidden; ++i) _hoWeight[i][0] = random.NextDouble();
        }

        public double Train(double[][] trainData, double learnRate)
        {
            double[] inputs = new double[_numInput];
            double error = 0.0;

            for(int i = 0; i < trainData.Length; i++)
            {
                double goalPrediction = trainData[i].Last();

                Array.Copy(trainData[i], inputs, _numInput);

                error += UpdateWeights(inputs, goalPrediction, learnRate);
            }

            return error;
        }

        public double ComputeOutputs(double[] inputs) => CreatePrediction(inputs);

        public (double[][], double[][]) GetWeights() => (_ihWeight, _hoWeight);
        
        public void SetWeights(double[][] ihWeight, double[][] hoWeight)
        {
            _ihWeight = ihWeight;
            _hoWeight = hoWeight;
        }

        public double UpdateWeights(double[] inputs, double goalPrediction, double learnRate)
        {
            double[] hiddenLayer = Helper.MultiplyMatrix(inputs, _ihWeight);
            double[] prediction = Helper.MultiplyMatrix(hiddenLayer, _hoWeight);

            double error = (prediction[0] - goalPrediction);
            double outputLayerDelta = prediction[0] - goalPrediction;
            double hiddenLayerDelta = Helper.MultiplyMatrix(outputLayerDelta, _hoWeight);


            for (int i = 0; i < _numInput; i++) //Коррекция весов входного и скрытого слоя
            {
                for (int j = 0; j < _numHidden; j++)
                {
                    _ihWeightDelta[i][j] = inputs[i] * hiddenLayerDelta;
                    _ihWeight[i][j] -= _ihWeightDelta[i][j] * learnRate;
                }
            }
            for (int i = 0; i < _numHidden; i++) //Коррекция весов скрытого и слоя вывода
            {
                _hoWeightDelta[i][0] = hiddenLayer[i] * outputLayerDelta;
                _hoWeight[i][0] -= _hoWeightDelta[i][0] * learnRate;
            }

            return Math.Pow(error, 2);
        }

        private double CreatePrediction(double[] inputs)
        {
            double[] hiddenLayer = Helper.MultiplyMatrix(inputs, _ihWeight);
            double[] prediction = Helper.MultiplyMatrix(hiddenLayer, _hoWeight);

            return prediction[0];
        }
    }
}
