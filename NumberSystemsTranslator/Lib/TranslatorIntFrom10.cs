using System;
using System.Linq;

namespace Lib
{
    /// <summary>
    /// Class, which translate number from 10 number system to given
    /// </summary>
    public class TranslatorIntFrom10 : Translator, ITranslatable
    {
        public TranslatorIntFrom10(int notion, string number) : base(notion, number)
        {

        }

        public string Translate()
        {
            string NewNumber = "";
            int _NumberInt = Convert.ToInt32(_Number);

            do
            {
                if (_NumberInt % _Notion < 10)
                {
                    NewNumber += (_NumberInt % _Notion).ToString();
                }
                else
                {
                    NewNumber += Convert.ToChar(_NumberInt % _Notion + 7 + '0');
                }

                _NumberInt /= _Notion;
            } while (_NumberInt != 0);

            return new string(NewNumber.ToCharArray().Reverse().ToArray()); ;
        }
    }
}
