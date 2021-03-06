using Lib;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NumberSystemsTranslator.ViewModels
{
    public class Variables : INotifyPropertyChanged
    {
        private string _NotionFrom = "";
        private string _NotionTo = "";
        private string _NumberEntire = "";
        private string _Result = "";

        public string NotionFrom { get { return _NotionFrom; } set { _NotionFrom = value; OnPropertyChanged(); } }
        public string NotionTo { get { return _NotionTo; } set { _NotionTo = value; OnPropertyChanged(); } }
        public string NumberEntire { get { return _NumberEntire; } set { _NumberEntire = value; OnPropertyChanged(); } }
        public string Result { get { return _Result; } set { _Result = value; OnPropertyChanged(); } }

        public Variables() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public CalculateCommand Calculate
        {
            get
            {
                return new CalculateCommand((obj) =>
                {
                    try
                    {
                        Result = Translator.Translate(NotionFrom, NotionTo, NumberEntire);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                });
            }
        }
    }
}
