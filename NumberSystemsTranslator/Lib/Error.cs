using System;
using System.Text.RegularExpressions;

namespace Lib
{
    public static class Error
    {
        public static string GetErrorMessage(string notionFrom, string notionTo, string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return "Вы не ввели число";
            }

            if (string.IsNullOrWhiteSpace(notionFrom))
            {
                return "Вы не ввели систему счисления, из которой будет осуществлен перевод";
            }

            if (string.IsNullOrWhiteSpace(notionTo))
            {
                return "Вы не ввели систему счисления, в которую будет осуществлен перевод";
            }

            if (number[0] == '0')
            {
                return "Число не должно начинаться с нуля";
            }

            if (number[0] == '.' || number[^1] == '.')
            {
                return "Точка должна быть между цифрами. Она не может быть ни первой, ни последней";
            }

            if (new Regex(@"[!@#№$%^&*()\-+=?<>/|\\ ]+").Matches(number).Count > 0)
            {
                return "Число не должно содержать что-то кроме букв или цифр";
            }

            if (new Regex(@"[А-Яа-я]+").Matches(number).Count > 0)
            {
                return "Не должно быть русских букв";
            }

            return "";
        }

        public static Exception ThrowError(string message)
        {
            throw new Exception(message: message);
        }
    }
}
