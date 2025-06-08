using System;
using System.Collections.Generic;
using System.Text;

namespace PersianTextShaper
{
    public static class PersianShaper
    {
        // تعریف فرم‌های مختلف حروف فارسی
        private static readonly Dictionary<char, string[]> LetterForms = new Dictionary<char, string[]>
        {
            // حرف: [تنها, ابتدا, وسط, انتها]
            ['ا'] = new[] { "\uFE8D", "\uFE8D", "\uFE8E", "\uFE8E" },
            ['ب'] = new[] { "\uFE8F", "\uFE91", "\uFE92", "\uFE90" },
            ['پ'] = new[] { "\uFB56", "\uFB58", "\uFB59", "\uFB57" },
            ['ت'] = new[] { "\uFE95", "\uFE97", "\uFE98", "\uFE96" },
            ['ث'] = new[] { "\uFE99", "\uFE9B", "\uFE9C", "\uFE9A" },
            ['ج'] = new[] { "\uFE9D", "\uFE9F", "\uFEA0", "\uFE9E" },
            ['چ'] = new[] { "\uFB7A", "\uFB7C", "\uFB7D", "\uFB7B" },
            ['ح'] = new[] { "\uFEA1", "\uFEA3", "\uFEA4", "\uFEA2" },
            ['خ'] = new[] { "\uFEA5", "\uFEA7", "\uFEA8", "\uFEA6" },
            ['د'] = new[] { "\uFEA9", "\uFEA9", "\uFEAA", "\uFEAA" },
            ['ذ'] = new[] { "\uFEAB", "\uFEAB", "\uFEAC", "\uFEAC" },
            ['ر'] = new[] { "\uFEAD", "\uFEAD", "\uFEAE", "\uFEAE" },
            ['ز'] = new[] { "\uFEAF", "\uFEAF", "\uFEB0", "\uFEB0" },
            ['ژ'] = new[] { "\uFB8A", "\uFB8A", "\uFB8B", "\uFB8B" },
            ['س'] = new[] { "\uFEB1", "\uFEB3", "\uFEB4", "\uFEB2" },
            ['ش'] = new[] { "\uFEB5", "\uFEB7", "\uFEB8", "\uFEB6" },
            ['ص'] = new[] { "\uFEB9", "\uFEBB", "\uFEBC", "\uFEBA" },
            ['ض'] = new[] { "\uFEBD", "\uFEBF", "\uFEC0", "\uFEBE" },
            ['ط'] = new[] { "\uFEC1", "\uFEC3", "\uFEC4", "\uFEC2" },
            ['ظ'] = new[] { "\uFEC5", "\uFEC7", "\uFEC8", "\uFEC6" },
            ['ع'] = new[] { "\uFEC9", "\uFECB", "\uFECC", "\uFECA" },
            ['غ'] = new[] { "\uFECD", "\uFECF", "\uFED0", "\uFECE" },
            ['ف'] = new[] { "\uFED1", "\uFED3", "\uFED4", "\uFED2" },
            ['ق'] = new[] { "\uFED5", "\uFED7", "\uFED8", "\uFED6" },
            ['ک'] = new[] { "\uFED9", "\uFEDB", "\uFEDC", "\uFEDA" },
            ['گ'] = new[] { "\uFB92", "\uFB94", "\uFB95", "\uFB93" },
            ['ل'] = new[] { "\uFEDD", "\uFEDF", "\uFEE0", "\uFEDE" },
            ['م'] = new[] { "\uFEE1", "\uFEE3", "\uFEE4", "\uFEE2" },
            ['ن'] = new[] { "\uFEE5", "\uFEE7", "\uFEE8", "\uFEE6" },
            ['و'] = new[] { "\uFEE9", "\uFEE9", "\uFEEA", "\uFEEA" },
            ['ه'] = new[] { "\uFEEB", "\uFEED", "\uFEEE", "\uFEEC" },
            ['ی'] = new[] { "\uFEF1", "\uFEF3", "\uFEF4", "\uFEF2" },
            ['ئ'] = new[] { "\uFE89", "\uFE8B", "\uFE8C", "\uFE8A" },
            ['ء'] = new[] { "\uFE80", "\uFE80", "\uFE80", "\uFE80" },
            ['آ'] = new[] { "\uFE81", "\uFE81", "\uFE82", "\uFE82" },
            ['أ'] = new[] { "\uFE83", "\uFE83", "\uFE84", "\uFE84" },
            ['ؤ'] = new[] { "\uFE85", "\uFE85", "\uFE86", "\uFE86" },
            ['إ'] = new[] { "\uFE87", "\uFE87", "\uFE88", "\uFE88" },
            ['ة'] = new[] { "\uFE93", "\uFE93", "\uFE94", "\uFE94" },
            ['ى'] = new[] { "\uFEEF", "\uFEEF", "\uFEF0", "\uFEF0" },
            ['ﻻ'] = new[] { "\uFEFB", "\uFEFB", "\uFEFC", "\uFEFC" },
        };

        // حروفی که به حروف بعدی یا قبلی متصل نمی‌شوند
        private static readonly HashSet<char> NonConnectingLetters = new HashSet<char>
        {
            'ا', 'د', 'ذ', 'ر', 'ز', 'ژ', 'و', 'آ', 'أ', 'ؤ', 'إ', 'ء', 'ة', 'ى', 'ﻻ'
        };

        public static string Fix(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var output = new StringBuilder();
            var words = input.Split(' ');

            foreach (var word in words.Reverse())
            {
                var shapedWord = new StringBuilder();
                var letters = word.ToCharArray();

                for (int i = 0; i < letters.Length; i++)
                {
                    char current = letters[i];
                    char? previous = i > 0 ? letters[i - 1] : (char?)null;
                    char? next = i < letters.Length - 1 ? letters[i + 1] : (char?)null;

                    bool connectsToPrevious = previous.HasValue && !NonConnectingLetters.Contains(previous.Value) && LetterForms.ContainsKey(current);
                    bool connectsToNext = next.HasValue && !NonConnectingLetters.Contains(current) && LetterForms.ContainsKey(next.Value);

                    int formIndex;
                    if (connectsToPrevious && connectsToNext)
                        formIndex = 2; // وسط
                    else if (connectsToPrevious)
                        formIndex = 3; // انتها
                    else if (connectsToNext)
                        formIndex = 1; // ابتدا
                    else
                        formIndex = 0; // تنها

                    if (LetterForms.ContainsKey(current))
                        shapedWord.Append(LetterForms[current][formIndex]);
                    else
                        shapedWord.Append(current);
                }

                // معکوس کردن کلمه برای نمایش صحیح در محیط‌های بدون پشتیبانی از راست به چپ
                var reversedWord = new string(shapedWord.ToString().ToCharArray().Reverse().ToArray());
                output.Append(reversedWord + " ");
            }

            return output.ToString().Trim();
        }
    }
}
