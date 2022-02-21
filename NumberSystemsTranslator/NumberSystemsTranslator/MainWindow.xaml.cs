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
        /// <summary>
        /// Required variables
        /// </summary>
        private string Number = "";
        private int NotionFrom = 0;
        private int NotionTo = 0;
        private string Result = "";

        //private NumberFormatInfo Provider = new()
        //{
        //    NumberDecimalSeparator = "."
        //};

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Number = TextBoxNumber.Text;

                string NotionFromStr = ComboBoxFrom.Text;
                string NotionToStr = ComboBoxTo.Text;

                //Checking and handling errors
                if (!string.IsNullOrWhiteSpace(GetErrorMessage()))
                {
                    ThrowError(GetErrorMessage());
                }

                NotionFrom = Convert.ToInt32(NotionFromStr);
                NotionTo = Convert.ToInt32(NotionToStr);
                Result = Translator.Translate(NotionFrom, NotionTo, Number);

                TextBoxResult.Text = Result;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private string GetErrorMessage()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNumber.Text))
            {
                return "Вы не ввели число";
            }

            if (string.IsNullOrWhiteSpace(ComboBoxFrom.Text))
            {
                return "Вы не ввели систему счисления, из которой будет осуществлен перевод";
            }

            if (string.IsNullOrWhiteSpace(ComboBoxTo.Text))
            {
                return "Вы не ввели систему счисления, в которую будет осуществлен перевод";
            }

            if (TextBoxNumber.Text[0] == '0')
            {
                return "Число не должно начинаться с нуля";
            }

            if (TextBoxNumber.Text[0] == '.' || TextBoxNumber.Text[^1] == '.')
            {
                return "Точка должна быть между цифрами. Она не может быть ни первой, ни последней";
            }

            if (new Regex(@"[a-z!@#№$%^&*()\-+=?<>/|\\ ]+").Matches(TextBoxNumber.Text).Count > 0)
            {
                return "Число не должно содержать что-то кроме букв или цифр";
            }

            if (new Regex(@"[А-Яа-я]+").Matches(TextBoxNumber.Text).Count > 0)
            {
                return "Не должно быть русских букв";
            }

            return "";
        }

        private static Exception ThrowError(string message)
        {
            throw new Exception(message: message);
        }
    }
}
