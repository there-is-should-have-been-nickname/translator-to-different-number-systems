using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class TranslatorFloatTo10 : Translator, ITranslatable
    {
        public TranslatorFloatTo10(int notion, string number) : base(notion, number)
        {
        }

        public string Translate()
        {
            double NewNumber = 0.0;
            double stepen = 1 / Convert.ToDouble(_Notion, Provider);

            for (int i = 2; i <= _Number.Length - 1; ++i) {
                if (_Number[i] <= 57)
                {
                    NewNumber += (Convert.ToInt32(_Number[i]) - '0') * stepen;
                }
                else
                {
                    NewNumber += (Convert.ToInt32(Char.ToUpper(_Number[i]) - '0') - 7) * stepen;
                }
                stepen /= _Notion;
            }
            return NewNumber.ToString(Provider);
        }
    }
}
