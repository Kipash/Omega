using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Console
{
    public static class Console
    {
        /*
        static InputField inputField;
        static Text log;
        static Char commandChar = '/';
        static string startString = "";


        static bool showTime;
        static Color typedCommandColor;
        static Color errorCommandColor;
        static Color defaultTextColor;
        static Color timeTextColor;
        */
        static ConsoleData data;
        static ConsoleCore consoleBase;

        public static void Initialize(ConsoleData data)
        {
            Console.data = data;

            consoleBase = new ConsoleCore(WriteLine) { };

            Console.WriteLine(data.StartMessage);
        }

        public static void Add(string name, object source, string methodName, bool argsToArray = false)
        {
            consoleBase.Add(name, source, methodName, argsToArray);
        }
        public static void AddStatic(Type type)
        {
            consoleBase.AddStatic(type);
        }
        public static void AddStatic(string name, Type type, string methodName, bool argsToArray = false)
        {
            consoleBase.AddStatic(name, type, methodName, argsToArray);
        }
        public static void Execute()
        {
            data.ConsoleInputField.ActivateInputField();
            if (string.IsNullOrEmpty(data.ConsoleInputField.text)) { }
            else if (data.ConsoleInputField.text[0] == data.CommandChar && data.ConsoleInputField.text != data.CommandChar.ToString())
            {
                SearchCommand(data.ConsoleInputField.text);
            }
            else
            {
                WriteLine(data.ConsoleInputField.text, true);
            }
            data.ConsoleInputField.text = "";
        }
        static void SearchCommand(string rawCommand)
        {
            WriteLine(" " + RitchTextHelper.Combine(rawCommand, RitchTextHelper.ColorToHex(data.TypedCommandColor), false, true));
            rawCommand = rawCommand.Substring(1, data.ConsoleInputField.text.Length - 1);
            if (string.IsNullOrEmpty(rawCommand.Substring(1, rawCommand.Length - 1)))
                WriteLine(RitchTextHelper.Combine("Unreco cmd", RitchTextHelper.ColorToHex(data.ErrorCommandColor), true, false));
            else
            {
                try
                {
                    consoleBase.Invoke(rawCommand);
                }
                catch (Exception e)
                {
                    var be = e.GetBaseException();
                    WriteLine(RitchTextHelper.Combine("Unreco cmd", RitchTextHelper.ColorToHex(data.ErrorCommandColor), true, false) + "(Details): " + be.Message);
                }
            }
        }
        public static void PrintErrorMessage(string errorMsg)
        {
            WriteLine(RitchTextHelper.Combine(errorMsg, RitchTextHelper.ColorToHex(data.ErrorCommandColor), true, false));
        }
        public static void PrintErrorMessage(string errorMsg, bool newline)
        {
            WriteLine(RitchTextHelper.Combine(errorMsg, RitchTextHelper.ColorToHex(data.ErrorCommandColor), true, false), newline);
        }
        public static void PrintErrorMessage(string errorMsg, string pasted)
        {
            WriteLine(RitchTextHelper.Combine(errorMsg, RitchTextHelper.ColorToHex(data.ErrorCommandColor), true, false) + ": " + pasted);
        }
        public static void PrintErrorMessage(string errorMsg, string pasted, bool newline)
        {
            WriteLine(RitchTextHelper.Combine(errorMsg, RitchTextHelper.ColorToHex(data.ErrorCommandColor), true, false) + ": " + pasted, newline);
        }
        public static void WriteLine(string line)
        {
            WriteLine(line, false);
        }
        public static void WriteLine(string line, bool Time)
        {
            //(newLine == true ? "\n" : "")
            data.ConsoleLog.text += "\n " +(Time == true ? 
                                             RitchTextHelper.Combine(
                                             "|" + DateTime.Now.ToString("HH:mm:ss") + "| ", RitchTextHelper.ColorToHex(data.TimeTextColor), false, true)
                                             : "" )
                                             
                                             + RitchTextHelper.DoColor(data.LineChar + line, RitchTextHelper.ColorToHex(data.DefaultTextColor));
        }

        public static void Help()
        {
            var cmds = consoleBase.GetAwaibleCommands();
            WriteLine("Avaible commands:", true);
            WriteLine("---- " + cmds.Length + " ----", false);
            foreach (var x in cmds)
                WriteLine(x, false);
            WriteLine("--------", false);
        }
        public static void ClearLog()
        {
            data.ConsoleLog.text = string.Empty;
        }
    }

    //class Program
    //{
    //    //static void Main() => Run();
    //    static void Main()
    //    {
    //        try
    //        {
    //            Run();
    //        }
    //        catch (Exception e)
    //        {
    //            System.Console.WriteLine(e);
    //        }
    //    }
    //
    //
    //    static void Run()
    //    {
    //        var console = new PlayerConsole();
    //
    //        //console.AddStatic(typeof(ConsoleFuncs));
    //        //console.AddStatic("cat", typeof(ConsoleFuncs), "Concat");
    //        
    //        //console.Add("cat", new ObjA(), "Concat", true);
    //        //console.Add("print", new ObjA(), "WriteLine");
    //        //
    //        //console.Add("log", new ObjB(), "Log", true);
    //
    //
    //        console.Run();
    //    }
    //}
}
