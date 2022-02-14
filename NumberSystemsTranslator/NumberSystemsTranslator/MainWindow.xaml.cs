using System;
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

        private TranslatorIntTo10 translatorIntTo10;
        private TranslatorIntFrom10 translatorIntFrom10;

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

                Result = Number;

                NotionFrom = Convert.ToInt32(NotionFromStr);
                NotionTo = Convert.ToInt32(NotionToStr);

                //Translating
                if (NotionFrom != NotionTo && NotionTo == 10 && NotionFrom != 10)
                {
                    translatorIntTo10 = new TranslatorIntTo10(NotionFrom, Number);
                    Result = translatorIntTo10.Translate();
                }
                else if (NotionFrom != NotionTo && NotionFrom == 10 && NotionTo != 10)
                {
                    translatorIntFrom10 = new TranslatorIntFrom10(NotionTo, Number);
                    Result = translatorIntFrom10.Translate();
                }
                else if (NotionFrom != NotionTo && NotionTo != 10 && NotionFrom != 10)
                {
                    translatorIntTo10 = new TranslatorIntTo10(NotionFrom, Number);
                    string tempInt = translatorIntTo10.Translate();
                    translatorIntFrom10 = new TranslatorIntFrom10(NotionTo, tempInt);
                    Result = translatorIntFrom10.Translate();
                }

                TextBoxResult.Text = Result;
                //TODO: ошибка, когда в записи есть недопустимые числа
                //TODO: ошибка, когда для дробей нельзя перевести

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
