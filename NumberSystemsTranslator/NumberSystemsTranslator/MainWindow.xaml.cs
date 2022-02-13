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
                if (CheckErrors())
                {
                    HandleErrors();
                }


                NotionFrom = Convert.ToInt32(NotionFromStr);
                NotionTo = Convert.ToInt32(NotionToStr);

                //Translating
                if (NotionTo == 10 && NotionFrom != 10)
                {
                    translatorIntTo10 = new TranslatorIntTo10(NotionFrom, Number);
                    Result = translatorIntTo10.Translate();
                }
                else if (NotionFrom == 10 && NotionTo != 10)
                {
                    translatorIntFrom10 = new TranslatorIntFrom10(NotionTo, Number);
                    Result = translatorIntFrom10.Translate();
                }
                else if (NotionTo != 10 && NotionFrom != 10)
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


        private bool CheckErrors()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNumber.Text))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(ComboBoxFrom.Text))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(ComboBoxTo.Text))
            {
                return true;
            }

            if (TextBoxNumber.Text[0] == '0')
            {
                return true;
            }

            if (new Regex(@"[a-z!@#№$%^&*()\-+=?<>/|\\ ]+").Matches(TextBoxNumber.Text).Count > 0)
            {
                return true;
            }

            return false;
        }

        private Exception HandleErrors()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNumber.Text))
            {
                throw new ArgumentNullException(paramName: "", message: "Вы не ввели число");
            }

            if (string.IsNullOrWhiteSpace(ComboBoxFrom.Text))
            {
                throw new ArgumentNullException(paramName: "", message: "Вы не ввели систему счисления, из которой будет осуществлен перевод");
            }

            if (string.IsNullOrWhiteSpace(ComboBoxTo.Text))
            {
                throw new ArgumentNullException(paramName: "", message: "Вы не ввели систему счисления, в которую будет осуществлен перевод");
            }

            if (TextBoxNumber.Text[0] == '0')
            {
                throw new ArgumentException(paramName: "", message: "Число не должно начинаться с нуля");
            }

            if (new Regex(@"[a-z!@#№$%^&*()\-+=?<>/|\\ ]+").Matches(TextBoxNumber.Text).Count > 0)
            {
                throw new ArgumentException(paramName: "", message: "Число не должно содержать что-то кроме букв или цифр");
            }

            throw new Exception(message: "Другая ошибка");
        }
    }
}
