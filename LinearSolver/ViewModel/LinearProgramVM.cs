using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LinearSolver.ViewModel
{
    class LinearProgramVM : DependencyObject
    {
        public string SolutionText
        {
            get { return (string)GetValue(LPSolutionTextProperty); }
            private set { SetValue(LPSolutionTextProperty, value); }
        }
        public static readonly DependencyProperty LPSolutionTextProperty =
        DependencyProperty.Register("LPSolutionText", typeof(string), typeof(LinearEquationVM));
    }
}
