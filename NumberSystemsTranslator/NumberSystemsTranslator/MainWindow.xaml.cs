using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Lib;

namespace NumberSystemsTranslator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Variables variables = new Variables();
        public MainWindow()
        {
            InitializeComponent();

            variables = new Variables();
            var bindingFrom = new Binding("NotionFrom")
            {
                Source = variables
            };
            var bindingTo = new Binding("NotionTo")
            {
                Source = variables
            };
            var bindingNumber = new Binding("NumberEntire")
            {
                Source = variables
            };

            ComboBoxFrom.SetBinding(ComboBox.TextProperty, bindingFrom);
            ComboBoxTo.SetBinding(ComboBox.TextProperty, bindingTo);
            TextBoxNumber.SetBinding(TextBox.TextProperty, bindingNumber);
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
