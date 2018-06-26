using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Задание_11
{
    class Program
    {
        static char[] _ru = {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у',
            'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
        };
        static char[] _en =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z'
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово для шифрования:");
            string word = StringTest();

            Console.WriteLine("Введите значение N:");
            int n = IntTest();

            string shiper = Coding(word, n);
            Console.WriteLine(String.Format($"Зашифрованное слово: {shiper}"));

            Console.WriteLine(String.Format($"Расшифрованное слово: {Coding(shiper, -n)}"));

            Console.ReadKey();
        }

        /// <summary>
        /// Шифрование слова
        /// </summary>
        /// <param name="word">Слово для шифрования</param>
        /// <param name="n">Смещение</param>
        /// <returns>Зашифрованное слово</returns>
        static string Coding(string word, int n)
        {
            string shiper = "";
            foreach (var letter in word)
            {
                shiper += Shiper(letter, n, new Regex("[A-Z]", RegexOptions.IgnoreCase).IsMatch(letter.ToString()) ? _en : _ru);
            }

            return shiper;
        }

        /// <summary>
        /// Замена буквы
        /// </summary>
        /// <param name="s">Символ для замены</param>
        /// <param name="n">Значение смещения</param>
        /// <param name="lang">Язык слова</param>
        /// <returns>Зашифрованная буква</returns>
        static char Shiper(char s, int n, char[] lang)
        {
            int code = Array.IndexOf(lang, Char.ToLower(s)) + n; //номер символа для шифрования
            if (code >= lang.Length) //если номер слишком большой
            {
                do { code -= lang.Length; } while (code >= lang.Length);
            }

            if (code < 0) //если номер слишком маленький
            {
                do { code += lang.Length; } while (code <= 0);
            }

            if (Char.IsLower(s)) return lang[code]; //если строчная буква
            else return Char.ToUpper(lang[code]); //если прописная буква
        }

        /// <summary>
        /// Проверка ввода строки
        /// </summary>
        /// <returns>Буквенная строка</returns>
        static string StringTest()
        {
            bool ok;
            string str = "";
            do
            {
                str = Console.ReadLine();
                ok = Regex.IsMatch(str, "[\\W\\d\\s]")  //есть ли в строке небуквенные символы 
                     || str == "";                      //пустая ли строка
                if (ok) { Console.WriteLine("Ошибка! В строке найдены недопустимые символы."); }
            } while (ok);

            return str;
        }

        /// <summary>
        /// Проверка ввода целого числа
        /// </summary>
        /// <returns>Целое число</returns>
        static int IntTest()
        {
            bool ok;
            int num;
            do
            {
                ok = Int32.TryParse(Console.ReadLine(), out num); //введено число? 
                if (!ok) { Console.WriteLine("Ошибка! Введите целое число."); }
            } while (!ok);

            return num;
        }
    }
}
