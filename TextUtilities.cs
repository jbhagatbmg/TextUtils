using System;
using System.Collections.Generic;
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

            return string.Join(Environment.NewLine, _list1.Except(_list2).Concat(_list1.Except(_list2)).Distinct());
        }

    }
}
