using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Extentions
{
    public class ConsoleManager
    {
        protected static StringBuilder Input = new StringBuilder();


        protected static void ClearCurrentLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private static void WriteLine()
        {
            string msg = Input.ToString();
            ClearCurrentLine();
            Console.Write(msg);
        }

        public static void WriteLine(string message)
        {
            lock (Input)
            {
                ConsoleKeyInfo c = default;

                if (message.ToLower() != "\r")
                {
                    //c = Console.ReadKey(true);
                    //switch (c.Key)                    {
                    //    case ConsoleKey.Backspace:
                    //        if (Input.Length > 0)
                    //            Input.Remove(Input.Length - 1, 1);
                    //        break;
                    //    default:
                    //        Input.Append(c.KeyChar);
                    //        break;
                    //}
                    Input.Append(message);
                    WriteLine();
                }
                else
                {
                    string res = Input.ToString();
                    Input.Clear();
                    WriteLine();
                }
            }
        }


        //public static void WriteLine(string message, ConsoleColor messageColor = ConsoleColor.White)
        //{
        //    // message = ReadLine(message);
        //    ClearCurrentLine();
        //    var pos = Console.GetCursorPosition();
        //    Console.ForegroundColor = messageColor;
        //    Console.SetCursorPosition(pos.Left, pos.Top);
        //    System.Console.WriteLine(message);

        //    pos = Console.GetCursorPosition();
        //    Console.SetCursorPosition(pos.Left, pos.Top);
        //    WriteLeadLine();

        //    pos = Console.GetCursorPosition();
        //    Console.ResetColor();
        //    Console.SetCursorPosition(pos.Left, pos.Top);
        //}
    }
}
