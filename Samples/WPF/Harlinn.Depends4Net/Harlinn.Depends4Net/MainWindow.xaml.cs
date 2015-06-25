using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Harlinn.Depends4Net.Types;

namespace Harlinn.Depends4Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommandBinding_CanExecuteOpenAssembly(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_ExecuteOpenAssembly(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                ((Depends)DataContext).AssemblyName = openFileDialog.FileName;
            }
        }


        private void CommandBinding_ExecuteClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is AssemblyNode)
            {
                ((Depends)DataContext).SelectedAssemblyNode = e.NewValue as AssemblyNode;
            }
            else
            {
                ((Depends)DataContext).SelectedAssemblyNode = null;
            }
        }

        

        

        

        

    }
}
