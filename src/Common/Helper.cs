namespace src.Common
{
    /// <summary>
    /// Вспомогательный класс со статическими методами для работы с матрицами.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Создает и возвращает матрицу размера rows x cols, заполненную нулями.
        /// </summary>
        public static double[][] MakeMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];

            for (int i = 0; i < rows; ++i)
            {
                result[i] = new double[cols];
            }

            return result;
        }

        public static double MultiplyMatrix(double[] matrix1, double[] matrix2)
        {
            double temp = 0.0;

            for (int i = 0; i < matrix1.Length; i++)
                temp += matrix1[i] * matrix2[i];

            return temp;
        } 

        public static double[] MultiplyMatrix(double num, double[] matrix)
        {
            double[] temp = new double[matrix.Length];

            for(int i = 0; i < matrix.Length; i++)
                temp[i] = matrix[i] * num;

            return temp;
        }

        public static double[][] MultiplyMatrix(double[][] matrix1, double[][] matrix2)
        {
            int rows = matrix1.Length;
            int columns = matrix2[0].Length;
            double[][] temp = MakeMatrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < rows; k++)
                        temp[i][j] += matrix1[i][k] * matrix2[k][j];
                }
            }

            return temp;
        }

        public static double[] MultiplyMatrix(double[] matrix1, double[][] matrix2)
        {
            int rows = matrix2.Length;
            int columns = matrix2[0].Length;
            double[] temp = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double sum = 0.0;

                for (int j = 0; j < columns; j++)
                {
                    sum += matrix1[j] * matrix2[i][j];
                }

                temp[i] = sum;
            }

            return temp;
        }

        public static double MultiplyMatrix(double num, double[][] matrix2)
        {
            int rows = matrix2.Length;
            int columns = matrix2[0].Length;
            double temp = 0.0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    temp += matrix2[i][j] * num;
            }

            return temp;
        }
    }
}
