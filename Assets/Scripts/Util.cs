using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Util : MonoBehaviour
{
    static public void LogColorRed(string str)
    {
        Debug.Log("<color=#ff0000>" + str + "</color>");
    }

    static public void LogColorYellow(string str)
    {
        Debug.Log("<color=#ffff00>" + str + "</color>");
    }

    static public void LogColorGreen(string str)
    {
        Debug.Log("<color=#00ff00>" + str + "</color>");
    }

    static public void LogColorLightGreen(string str)
    {
        Debug.Log("<color=#00ffff>" + str + "</color>");
    }

    static public void LogColorMagenta(string str)
    {
        Debug.Log("<color=#ff00ff>" + str + "</color>");
    }

    static public long GetUnixTime()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}
