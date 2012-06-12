using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;
using LinearSolver.ViewModel;
using System.Globalization;

namespace LinearSolver.View
{
    /// <summary>
    /// Interaction logic for LinearEquationView.xaml
    /// </summary>
    public partial class LinearEquationView : UserControl
    {
        LinearEquationVM _vm;

        public LinearEquationView()
        {
            InitializeComponent();
            _vm = base.DataContext as LinearEquationVM;
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            _vm.Solve();
        }
    }

    public class ParenScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var matrix = (List<List<DependencyDouble>>)value;
            return matrix.Count * 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
