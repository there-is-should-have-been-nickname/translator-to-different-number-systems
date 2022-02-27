using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lib
{
    public class Variables
    {
        public string NotionFrom { get; set; }
        public string NotionTo { get; set; }
        public string NumberEntire { get; set; }
        public string Result { get; set; }

        public Variables() { }

        public void ButtonCalculateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Result = Translator.Translate(NotionFrom, NotionTo, NumberEntire);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
