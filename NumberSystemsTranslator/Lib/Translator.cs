using System.Globalization;
using System;

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

            if (!IsCorrectDigits())
            {
                throw new Exception(message: "В записи числа присутствуют несоответствующие цифры");
            }
        }

        private bool IsCorrectDigits()
        {
            for (int i = 0; i < _Number.Length; ++i)
            {
                if (Convert.ToInt32(_Number[i]) >= _Notion)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
