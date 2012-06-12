using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinearSolver.Model;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace LinearSolver.ViewModel
{
    class LinearEquationVM : DependencyObject
    {
        LinearEquationSolver _model;
        
        public LinearEquationVM()
        {
            _model = new LinearEquationSolver();
            //this in turn rebuilds coefficient matrix
            NUnknown = 4;
        }

        private void RebuildCoefficientMatrix()
        {
            //build as temp object so UI does not update too soon
            var tmpMatrix = new List<List<DependencyDouble>>();
            BVector = new List<DependencyDouble>();
            Unknowns = new List<string>();

            for (int i = 0; i != NUnknown; ++i)
            {
                tmpMatrix.Add(new List<DependencyDouble>());
                for (int j = 0; j != NUnknown; ++j)
                    tmpMatrix[i].Add(new DependencyDouble());

                BVector.Add(new DependencyDouble());

                char unknown = (char)(i + Convert.ToInt32('a'));
                Unknowns.Add(new string(unknown, 1));
            }

            CoefficientMatrix = tmpMatrix;
        }

        public void Solve()
        {
            Matrix coefficients = new Matrix(NUnknown.Value, NUnknown.Value);

            for (int i = 0; i != CoefficientMatrix.Count; ++i)
            {
                for (int j = 0; j != CoefficientMatrix[i].Count; ++j)
                    coefficients[i, j] = CoefficientMatrix[i][j].Val;
            }

            //xxx check for nulls
            double[] b = BVector.Select(x=> x.Val).ToArray();
            double[] solution;
            bool success = _model.Solve(coefficients, b, out solution);

            if (success)
            {
                var builder = new StringBuilder();
                for (int i = 0; i != solution.Length; ++i)
                    builder.Append(Unknowns[i] + " = " + solution[i] + "\n");
                SolutionText = builder.ToString();
            }
            else
                SolutionText = "Singular coefficient matrix";
        }

        public Nullable<int> NUnknown
        {
            get { return (Nullable<int>)GetValue(NUnknownProperty); }
            set { SetValue(NUnknownProperty, value); }
        }
        public static readonly DependencyProperty NUnknownProperty =
        DependencyProperty.Register("NUnknown", typeof(Nullable<int>), typeof(LinearEquationVM),
        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(NUnknownChanged)));

        private static void NUnknownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LinearEquationVM vm = (LinearEquationVM)d;
            Nullable<int> newVal = (Nullable<int>)e.NewValue;
            if (newVal.HasValue)
            {
                vm.RebuildCoefficientMatrix();
            }
        }

        public List<List<DependencyDouble>> CoefficientMatrix
        {
            get { return (List<List<DependencyDouble>>)GetValue(CoefficientMatrixProperty); }
            private set { SetValue(CoefficientMatrixProperty, value); }
        }        
        public static readonly DependencyProperty CoefficientMatrixProperty =
        DependencyProperty.Register("CoefficientMatrix", typeof(List<List<DependencyDouble>>), typeof(LinearEquationVM));

        public List<DependencyDouble> BVector
        {
            get { return (List<DependencyDouble>)GetValue(BVectorProperty); }
            private set { SetValue(BVectorProperty, value); }
        }
        public static readonly DependencyProperty BVectorProperty =
        DependencyProperty.Register("BVector", typeof(List<DependencyDouble>), typeof(LinearEquationVM));

        public List<string> Unknowns
        {
            get { return (List<string>)GetValue(UnknownsProperty); }
            private set { SetValue(UnknownsProperty, value); }
        }
        public static readonly DependencyProperty UnknownsProperty =
        DependencyProperty.Register("Unknowns", typeof(List<string>), typeof(LinearEquationVM));

        public string SolutionText
        {
            get { return (string)GetValue(SolutionTextProperty); }
            private set { SetValue(SolutionTextProperty, value); }
        }
        public static readonly DependencyProperty SolutionTextProperty =
        DependencyProperty.Register("SolutionText", typeof(string), typeof(LinearEquationVM));
    }
}
