using System;

namespace Lib
{
    public class TranslatorFloatFrom10 : Translator, ITranslatable
    {


        public TranslatorFloatFrom10(int notion, string number) : base(notion, number)
        {

        }

        public string Translate()
        {
            string NewNumber = "0.";
            double _NumberFloat = Convert.ToDouble(_Number, Provider);

            do
            {
                _NumberFloat *= Convert.ToDouble(_Notion);
                int NumberCopy = (Math.Round(_NumberFloat) >= _NumberFloat) && (Math.Round(_NumberFloat) - _NumberFloat > 0.000000001)
                    ? Convert.ToInt32(_NumberFloat)
                    : Convert.ToInt32(Math.Round(_NumberFloat));

                if (Math.Truncate(_NumberFloat) < 10)
                {
                    NewNumber += NumberCopy.ToString();
                }
                else
                {
                    NewNumber += Convert.ToChar(Convert.ToInt32(NumberCopy + 7 + '0'));
                }

                _NumberFloat -= NumberCopy;
            } while ((_NumberFloat.ToString(Provider) != "0.000000") && (_NumberFloat > 0));
            return NewNumber;
        }
    }
}
