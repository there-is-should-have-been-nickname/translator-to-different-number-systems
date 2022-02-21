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
                //Checking and handling errors
                if (!string.IsNullOrWhiteSpace(Error.GetErrorMessage(ComboBoxFrom.Text, ComboBoxTo.Text, TextBoxNumber.Text)))
                {
                    Error.ThrowError(Error.GetErrorMessage(ComboBoxFrom.Text, ComboBoxTo.Text, TextBoxNumber.Text));
                }

                TextBoxResult.Content = Translator.Translate(Convert.ToInt32(ComboBoxFrom.Text), 
                    Convert.ToInt32(ComboBoxTo.Text), 
                    TextBoxNumber.Text);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
