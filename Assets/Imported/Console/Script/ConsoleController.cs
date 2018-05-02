using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Console;

[Serializable]
public class ConsoleData
{
    public string StartMessage;
    public string LineChar;
    public bool ShowTime;
    public Color TypedCommandColor;
    public Color ErrorCommandColor;
    public Color DefaultTextColor;
    public Color TimeTextColor;
    public Char CommandChar;
    public InputField ConsoleInputField;
    public Text ConsoleLog;
}

public class ConsoleController : MonoBehaviour
{
    public ConsoleData Data;
    
    
    //Console.Console console;
    public Animator ConsoleAnimotor;
    void Start ()
    {
        Console.Console.Initialize(Data);
        //console.consoleBase.AddStatic();
        Console.Console.AddStatic("help", typeof(Console.Console), "Help");
        Console.Console.AddStatic("clear", typeof(Console.Console), "ClearLog");
        Console.Console.AddStatic("cls", typeof(Console.Console), "ClearLog");
        
    }
    bool active = false;
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Console.Console.Execute();

        if (Input.GetKeyDown(KeyCode.BackQuote) && (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)))
        {
            if (active)
            {
                //ConsoleInputField.inte
                Data.ConsoleInputField.DeactivateInputField();
                ConsoleAnimotor.SetTrigger("Pull");
            }
            else
            {
                ConsoleAnimotor.SetTrigger("Push");
                Data.ConsoleInputField.ActivateInputField();
                Data.ConsoleInputField.text = "";
            }
            active = !active;
            //Console.Console.WriteLine("Console is " + active);
        }
    }
}
