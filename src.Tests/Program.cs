using src.Common;
using src.Services;

double[] input = new[]
{
    1, 0, 1, 0.5, 0.1, 0.442, 0.3
};
int numInput = 7;
int numHidden = 7;
double learnRate = 0.001;
double errorThresh = 0.001;

int count = 0;
double error = double.MaxValue;
double[][] ihWeights = new double[7][];
double[][] hoWeights = new double[7][];

while (count < 50 && error > errorThresh || double.IsNaN(error))
{
    Console.WriteLine($"Вызов нейронки: {count}");

    NeuralNetworkService nnRun = new(numInput, numHidden);
    nnRun.InitializeWeights();

    int epoch = 0;
    double minimalError = double.MaxValue;
    double[][] tempIHWeights = new double[numInput][];
    double[][] tempHOWeights = new double[numInput][];

    while (epoch < 500000 && minimalError > errorThresh)
    {
        double tempError = nnRun.Train(InputDataMLP.Data, learnRate);

        if (double.IsNaN(tempError)) break;

        if(tempError < minimalError)
        {
            (tempIHWeights, tempHOWeights) = nnRun.GetWeights();

            minimalError = tempError;
        }

        epoch++;
    }

    if(minimalError < error)
    {
        nnRun.SetWeights(tempIHWeights, tempHOWeights);
        double res = nnRun.ComputeOutputs(input);

        if (!double.IsNaN(res))
        {
            error = minimalError;
            hoWeights = tempHOWeights;
            ihWeights = tempIHWeights;
            Console.WriteLine($"error: {error}");
            Console.WriteLine($"middle res: {res}");
        }
    }

    count++;
}

NeuralNetworkService nn = new(numInput, numHidden);
nn.SetWeights(ihWeights, hoWeights);

Console.WriteLine($"Результат: {nn.ComputeOutputs(input) * 10000000}");