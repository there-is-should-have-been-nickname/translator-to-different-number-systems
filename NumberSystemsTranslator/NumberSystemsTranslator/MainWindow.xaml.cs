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
        private string NumberEntire = "";
        private string NumberFractional = "";
        private int NotionFrom = 0;
        private int NotionTo = 0;
        private string Result = "";

        private TranslatorIntTo10 translatorIntTo10;
        private TranslatorIntFrom10 translatorIntFrom10;
        private TranslatorFloatTo10 translatorFloatTo10;
        private TranslatorFloatFrom10 translatorFloatFrom10;

        private NumberFormatInfo Provider = new()
        {
            NumberDecimalSeparator = "."
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NumberEntire = TextBoxNumber.Text;

                string NotionFromStr = ComboBoxFrom.Text;
                string NotionToStr = ComboBoxTo.Text;

                //Checking and handling errors
                if (!string.IsNullOrWhiteSpace(GetErrorMessage()))
                {
                    ThrowError(GetErrorMessage());
                }

                Result = NumberEntire;

                NotionFrom = Convert.ToInt32(NotionFromStr);
                NotionTo = Convert.ToInt32(NotionToStr);

                //Translating
                if (NotionFrom != NotionTo)
                {
                    if (IsFloat(NumberEntire))
                    {
                        NumberFractional = "0." + NumberEntire.Split(".")[1];
                        NumberEntire = NumberEntire.Split(".")[0];

                        string ResultInt = GetIntValue();
                        string ResultFloat = GetFloatValue();
                        Result = (Convert.ToDouble(ResultInt, Provider) + Convert.ToDouble(ResultFloat, Provider)).ToString(Provider);
                    } else
                    {
                        string ResultInt = GetIntValue();
                        Result = ResultInt;
                    }
                }

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

            return "";
        }

        private static Exception ThrowError(string message)
        {
            throw new Exception(message: message);
        }

        private static bool IsFloat(string num)
        {
            if (new Regex(@"\.+").Matches(num).Count > 0)
            {
                return true;
            }
            return false;
        }

        private string GetIntValue() {
            translatorIntTo10 = new TranslatorIntTo10(NotionFrom, NumberEntire);
            string tempInt = translatorIntTo10.Translate();
            translatorIntFrom10 = new TranslatorIntFrom10(NotionTo, tempInt);
            return translatorIntFrom10.Translate();
        }

        private string GetFloatValue()
        {
            translatorFloatTo10 = new TranslatorFloatTo10(NotionFrom, NumberFractional);
            string tempFloat = translatorFloatTo10.Translate();
            translatorFloatFrom10 = new TranslatorFloatFrom10(NotionTo, tempFloat);
            return translatorFloatFrom10.Translate();
        }

        private void TextBoxNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (new Regex(@"[А-Яа-я]+").Matches(TextBoxNumber.Text).Count > 0)
            {
                MessageBox.Show("Зай, ну давай хоть тут без могучего русского");
                TextBoxNumber.Text = "";
            }
        }
    }
}
