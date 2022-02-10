using System;

namespace NumberSystemsCalculator
{
    /// <summary>
    /// Class, which translate number from given number system to 10
    /// </summary>
    public class TranslatorIntTo10 : Translator, ITranslatable
    {
        public TranslatorIntTo10(int notion, string number) : base(notion, number)
        {

        }

        public string Translate()
        {
            int NewNumber = 0;
            for (int i = 0; i < _Number.Length; ++i)
            {
                NewNumber = _Number[i] <= 57
                    ? (NewNumber * _Notion) + Convert.ToInt32(_Number[i] - '0')
                    : (NewNumber * _Notion) + Convert.ToInt32(_Number[i] - '0' - 7);
            }
            return NewNumber.ToString();
        }
    }
}
