using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextUtils
{
    public static class TextUtilities
    {
        public static string DecodeBase64(string input)
        {
            byte[] data = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(data);
        }
        public static string ReverseString(string input)
        {
            StringBuilder _reversed = new StringBuilder();
            for (var i = input.Length - 1; i >= 0; i--)
            {
                _reversed.Append(input[i]);
            }
            return _reversed.ToString();
        }

        public static async Task DecodeBase64Binary(string input, string dest)
        {
            byte[] data = Convert.FromBase64String(input);
            using (var _file = File.OpenWrite(dest))
            {
                await new MemoryStream(data).CopyToAsync(_file);
            }
        }


        public static string EncodeBase64(string text)
        {
            return (Convert.ToBase64String(Encoding.UTF8.GetBytes(text), Base64FormattingOptions.None));
        }
        public static string UniqueDiffLines(string text1, string text2)
        {
            var _list1 = text1.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.ToUpperInvariant()).ToList();
            var _list2 = text2.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.ToUpperInvariant()).ToList();
            Console.WriteLine($"List 1 Length: {_list1.Count}");
            Console.WriteLine($"List 2 Length: {_list2.Count}");
            Console.WriteLine($"Diff Length: {Math.Abs(_list1.Count - _list2.Count)}");

            return string.Join(Environment.NewLine, _list1.ExclusiveOr(_list2).ToList());
        }
        public static IList<T> ExclusiveOr<T>([NotNull]this IList<T> @this, IList<T> a)
        {
            a = a ?? new T[] { };
            ISet<T> set = new HashSet<T>(@this);
            foreach (T current in a)
            {
                if (set.Contains(current))
                {
                    set.Remove(current);
                }
                else
                {
                    set.Add(current);
                }
            }
            return set.ToList();
        }
    }
}
