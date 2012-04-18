using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearSolver.Model
{
    class LinearEquationSolver
    {
        public bool Solve(Matrix matrix, double[] b, out double[] solution)
        {
            int[] permutation;
            bool success = matrix.LUPDecompose(out permutation);
            solution = new double[matrix.Rows];

            if (!success)
                return false;

            var y = new double[matrix.Rows];
            for (int i = 0; i != matrix.Rows; ++i)
            {
                double sum = 0;
                for (int j = 0; j < i - 1; ++j)
                    sum += matrix[i, j] * y[j];
                y[i] = b[permutation[i]] - sum;
            }

            for (int i = matrix.Rows - 1; i != -1; --i)
            {
                double sum = 0;
                for (int j = i + 1; j != matrix.Rows; ++j)
                    sum += matrix[i, j] * solution[j];
                solution[i] = (y[i] - sum) / matrix[i, i];
            }

            return true;
        }
    }
}
