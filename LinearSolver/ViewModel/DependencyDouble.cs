using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LinearSolver.ViewModel
{
    class DependencyDouble : DependencyObject
    {
        public double Val
        {
            get { return (double)GetValue(ValProperty); }
            set { SetValue(ValProperty, value); }
        }
        public static readonly DependencyProperty ValProperty =
        DependencyProperty.Register("Val", typeof(double), typeof(DependencyDouble));
    }
}
