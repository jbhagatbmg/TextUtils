using System;
using System.Text.Encodings.Web;
using System.Web;
using TextCopy;

namespace TextUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            int func = -1;
            do
            {
                try
                {
                    string menu = @"
            Clipboard Transform Actions:

            -1: Quit
            1: Base64 Decode
            2: Base64 Encode
            3: HTML Decode
            4: HTML Encode
            5: Unique Difference In Lists
            ";
                    Console.WriteLine(menu);
                    Console.Write("Enter Action: ");
                    func = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (func)
                    {
                        case 1:
                            Clipboard.SetText(TextUtilities.DecodeBase64(Clipboard.GetText()));
                            break;
                        case 2:
                            Clipboard.SetText(TextUtilities.EncodeBase64(Clipboard.GetText()));
                            break;
                        case 3:
                            Clipboard.SetText(HttpUtility.HtmlDecode(Clipboard.GetText()));
                            break;
                        case 4:
                            Clipboard.SetText(HtmlEncoder.Default.Encode(Clipboard.GetText()));
                            break;
                        case 5:
                            Console.Write("Confirm paste first set [ENTER]:");
                            Console.ReadLine();
                            string _set1 = Clipboard.GetText();
                            Console.Write("Confirm paste second set [ENTER]:");
                            Console.ReadLine();
                            string _set2 = Clipboard.GetText();
                            Clipboard.SetText(TextUtilities.UniqueDiffLines(_set1, _set2));
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (func != -1);
        }
    }
}
