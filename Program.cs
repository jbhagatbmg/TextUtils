using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
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
            1: Base64 Decode (Binary)
            2: Base64 Encode
            3: HTML Decode
            4: HTML Encode
            5: Unique Difference In Lists
            6: JSON Format
            ";
                    Console.WriteLine(menu);
                    Console.Write("Enter Action: ");
                    try
                    {
                        func = int.Parse(Console.ReadLine());
                    }
                    catch { continue; }

                    Console.Clear();
                    if (_textFunctions.ContainsKey(func))
                    {
                        _textFunctions[func]();
                    }
                    else
                    {
                        throw  new Exception("Invalid function");
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
            _textFunctions.Add(1, () => Clipboard.SetText(TextUtilities.DecodeBase64(Clipboard.GetText())));
            _textFunctions.Add(2, () =>
                Clipboard.SetText(TextUtilities.EncodeBase64(Clipboard.GetText()))
            );
            _textFunctions.Add(3, () =>
                Clipboard.SetText(HttpUtility.HtmlDecode(Clipboard.GetText()))
            );
            _textFunctions.Add(4, () =>
                Clipboard.SetText(HtmlEncoder.Default.Encode(Clipboard.GetText()))
            );
            _textFunctions.Add(5, () =>
            {
                Console.Write("Confirm paste first set [ENTER]:");
                Console.ReadLine();
                string _set1 = Clipboard.GetText();
                Console.Write("Confirm paste second set [ENTER]:");
                Console.ReadLine();
                string _set2 = Clipboard.GetText();
                Clipboard.SetText(TextUtilities.UniqueDiffLines(_set1, _set2));
            });
            _textFunctions.Add(6, () =>
                Clipboard.SetText(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Clipboard.GetText()), Formatting.Indented)));

        }
    }
}
