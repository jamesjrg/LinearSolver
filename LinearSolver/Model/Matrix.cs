using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearSolver.Model
{
    class Matrix
    {
        public double[,] matrix;

        public Matrix(int rows, int cols)
        {
            matrix = new double[rows, cols];
        }

        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }

        public int Rows
        {
            get { return matrix.GetLength(0); }
        }

        public int Cols
        {
            get { return matrix.GetLength(1); }
        }

        /*
         * returns true if successful, false if a singular matrix
         * 
         * decomposed in place, with resultant matrix representing both L and U
         * lu[i,j] = l[i,j] if i > j
         * lu[i,j] = u[i,j] if i <= j
         * l always has 1s across diagonal, so no need to calculate or store

         * P is represented as an array, where permutation[i] = j means the ith row of P contains a 1 in column j       
         */
        public bool LUPDecompose(out int[] permutation)
        {
            permutation = new int[this.Rows];
            for (int i = 0; i != permutation.Length; ++i)
                permutation[i] = i;

            for (int k = 0; k != this.Rows; ++k)
            {
                double p = 0;
                int kPrime = 0;
                for (int i = k; i != this.Rows; ++i)
                {
                    if (this[i, k] > p)
                    {
                        p = this[i, k];
                        kPrime = i;
                    }
                }
                if (p == 0)
                    return false;

                int tmp = permutation[k];
                permutation[k] = permutation[kPrime];
                permutation[kPrime] = tmp;

                for (int i = 0; i != this.Rows; ++i)
                {
                    double tmpDouble = this[k, i];
                    this[k, i] = this[kPrime, i];
                    this[kPrime, i] = tmpDouble;
                }
                for (int i = k + 1; i != this.Rows; ++i)
                {
                    this[i, k] = this[i, k] / this[k, k];
                    for (int j = k + 1; j != this.Rows; ++j)
                        this[i, j] -= this[i, k] * this[k, j];
                }
            }

            return true;
        }
    }
}
