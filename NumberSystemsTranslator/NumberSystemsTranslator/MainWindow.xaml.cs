using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using Lib;

namespace NumberSystemsTranslator
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

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBoxResult.Content = Translator.Translate(ComboBoxFrom.Text, ComboBoxTo.Text, TextBoxNumber.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
