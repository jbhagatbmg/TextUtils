using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using TextCopy;

namespace TextUtils
{
    class Program
    {
        static Dictionary<int, Action> _textFunctions = new Dictionary<int, Action>();

        static void Main(string[] args)
        {
            int func = -1;
            InitFunctions();
            do
            {
                try
                {
                    string menu = @"
            Clipboard Transform Actions:

            -1: Quit
            1: Base64 Decode (UTF-8)
            2: Base64 Decode (Binary)
            3: Base64 Encode
            4: HTML Decode
            5: HTML Encode
            6: Distinct
            7: XOR Lists
            8: Regex Unescape
            9: JSON Format
            10: Reverse String
            11: URL Encode
            12: URL Decode
            ";
                    Console.WriteLine(menu);
                    Console.Write("Enter Action: ");
                    try
                    {
                        func = int.Parse(Console.ReadLine());
                    }
                    catch { continue; }

                    if (func == -1)
                    {
                        break;
                    }
                    Console.Clear();
                    if (_textFunctions.ContainsKey(func))
                    {
                        _textFunctions[func]();
                    }
                    else
                    {
                        throw new Exception("Invalid function");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (func != -1);
        }

        private static void InitFunctions()
        {
            void DecodeBase64Text() => Clipboard.SetText(TextUtilities.DecodeBase64(Clipboard.GetText()));

            _textFunctions.Add(1, DecodeBase64Text);

            void DecodeBase64Binary()
            {
                Console.Write("Output file: ");
                string _dest = Console.ReadLine();
                TextUtilities.DecodeBase64Binary(Clipboard.GetText(), _dest);
            }

            _textFunctions.Add(2, DecodeBase64Binary);

            void EncodeBase64() => Clipboard.SetText(TextUtilities.EncodeBase64(Clipboard.GetText()));

            _textFunctions.Add(3, EncodeBase64
            );

            void HTMLDecode() => Clipboard.SetText(HttpUtility.HtmlDecode(Clipboard.GetText()));

            _textFunctions.Add(4, HTMLDecode
            );

            void HTMLEncode() => Clipboard.SetText(HtmlEncoder.Default.Encode(Clipboard.GetText()));

            _textFunctions.Add(5, HTMLEncode
            );

            void Distinct()
            {
                string _clip = Clipboard.GetText();

                if (!string.IsNullOrWhiteSpace(_clip))
                {
                    Clipboard.SetText(string.Join(Environment.NewLine, _clip.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).GroupBy(a => a).Select(a => a.First())));
                }
                else
                {
                    Console.WriteLine("Empty clipboard");
                }
            }
            void XORList()
            {
                Console.Write("Confirm paste first set [ENTER]:");
                Console.ReadLine();
                string _set1 = Clipboard.GetText();
                Console.Write("Confirm paste second set [ENTER]:");
                Console.ReadLine();
                string _set2 = Clipboard.GetText();
                var _res = TextUtilities.UniqueDiffLines(_set1, _set2);
                if (!string.IsNullOrWhiteSpace(_res))
                {
                    Clipboard.SetText(_res);
                }
                else
                {
                    Console.WriteLine("Empty output");
                }
            }

            _textFunctions.Add(6, Distinct);
            _textFunctions.Add(7, XORList);
            _textFunctions.Add(8, () =>
                Clipboard.SetText(Regex.Unescape(Clipboard.GetText())));
            _textFunctions.Add(9, () =>
                Clipboard.SetText(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Clipboard.GetText()), Formatting.Indented)));

            _textFunctions.Add(10, () =>
                Clipboard.SetText(TextUtilities.ReverseString(Clipboard.GetText())));

            _textFunctions.Add(11, () =>
                Clipboard.SetText(HttpUtility.UrlEncode(Clipboard.GetText())));

            _textFunctions.Add(12, () =>
                Clipboard.SetText(HttpUtility.UrlDecode(Clipboard.GetText())));

        }


    }
}
