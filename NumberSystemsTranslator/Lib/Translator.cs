using System.Globalization;

namespace Lib
{
    /// <summary>
    /// Abstract class which contains notion and number in strings
    /// </summary>
    public abstract class Translator
    {
        protected int _Notion = 0;
        protected string _Number = "";
        protected NumberFormatInfo Provider;

        public Translator(int notion, string number)
        {
            _Notion = notion;
            _Number = number;
            Provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
        }
    }
}
