using System.Globalization;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lib
{
    public static class Translator
    {
        private static int _NotionFrom = 0;
        private static int _NotionTo = 0;
        private static string _NumberEntire = "";
        private static string _NumberFractional = "";
        private static readonly NumberFormatInfo Provider= new()
        {
            NumberDecimalSeparator = "."
        };

        private static bool IsCorrectDigits()
        {
            for (int i = 0; i < _NumberEntire.Length; ++i)
            {
                if (_NumberEntire[i] <= 57 && (Convert.ToInt32(_NumberEntire[i]) - '0') >= _NotionFrom)
                {
                    return false;
                }
                if (_NumberEntire[i] > 57 && (Convert.ToInt32(_NumberEntire[i] - '0' - 7) >= _NotionFrom)) {
                    return false;
                }
            }
            return true;
        }

        private static string TranslateIntTo10()
        {
            ulong NewNumber = 0;
            for (int i = 0; i < _NumberEntire.Length; ++i)
            {
                NewNumber = _NumberEntire[i] <= 57
                    ? (NewNumber * (ulong)_NotionFrom) + Convert.ToUInt64(_NumberEntire[i] - '0')
                    : (NewNumber * (ulong)_NotionFrom) + Convert.ToUInt64(_NumberEntire[i] - '0' - 7);
            }
            return NewNumber.ToString();
        }

        private static string TranslateIntFrom10()
        {
            string NewNumber = "";
            ulong _NumberInt = Convert.ToUInt64(_NumberEntire);

            do
            {
                if (_NumberInt % (ulong)_NotionTo < 10)
                {
                    NewNumber += (_NumberInt % (ulong)_NotionTo).ToString();
                }
                else
                {
                    NewNumber += Convert.ToChar(_NumberInt % (ulong)_NotionTo + 7 + '0');
                }

                _NumberInt /= (ulong)_NotionTo;
            } while (_NumberInt != 0);

            return new string(NewNumber.ToCharArray().Reverse().ToArray());
        }

        private static string TranslateFloatTo10()
        {
            double NewNumber = 0.0;
            double stepen = 1 / Convert.ToDouble(_NotionFrom, Provider);

            for (int i = 2; i <= _NumberFractional.Length - 1; ++i)
            {
                if (_NumberFractional[i] <= 57)
                {
                    NewNumber += (Convert.ToInt32(_NumberFractional[i]) - '0') * stepen;
                }
                else
                {
                    NewNumber += (Convert.ToInt32(Char.ToUpper(_NumberFractional[i]) - '0') - 7) * stepen;
                }
                stepen /= _NotionFrom;
            }
            return NewNumber.ToString(Provider);
        }

        private static string TranslateFloatFrom10()
        {
            string NewNumber = "0.";
            double _NumberFloat = Convert.ToDouble(_NumberFractional, Provider);
            int DigitsLimit = 0;

            do
            {
                _NumberFloat *= Convert.ToDouble(_NotionTo);
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
                ++DigitsLimit;
            } while ((_NumberFloat.ToString(Provider) != "0.000000") && (_NumberFloat > 0) && DigitsLimit <= 10);
            return NewNumber;
        }

        private static void SetParamenters(int notionFrom, int notionTo, string number)
        {
            _NotionFrom = notionFrom;
            _NotionTo = notionTo;
            _NumberEntire = number.ToUpper();
        }

        private static bool IsFloat(string num)
        {
            if (new Regex(@"\.+").Matches(num).Count > 0)
            {
                return true;
            }
            return false;
        }

        private static string GetIntValue()
        {
            string tempInt = TranslateIntTo10();
            _NumberEntire = tempInt;
            return TranslateIntFrom10();
        }

        private static string GetFloatValue()
        {
            string tempFloat = TranslateFloatTo10();
            _NumberFractional = tempFloat;
            return TranslateFloatFrom10();
        }

        public static string Translate(int notionFrom, int notionTo, string number)
        {
            SetParamenters(notionFrom, notionTo, number);

            if (!IsCorrectDigits())
            {
                throw new Exception(message: $"Минимум дна из цифр не входит в систему счисления с основанием {notionFrom}");
            }

            if (_NotionFrom != _NotionTo)
            {
                if (IsFloat(_NumberEntire))
                {
                    _NumberFractional = "0." + _NumberEntire.Split(".")[1];
                    _NumberEntire = _NumberEntire.Split(".")[0];

                    string ResultInt = GetIntValue();
                    string ResultFloat = GetFloatValue();
                    return (Convert.ToDouble(ResultInt, Provider) + Convert.ToDouble(ResultFloat, Provider)).ToString(Provider);
                }
                else
                {
                    string ResultInt = GetIntValue();
                    return ResultInt;
                }
            }
            return _NumberEntire;
        }
    }
}
