using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SaveCommonData
{
    public static void Save(string file,float value)
    {
        PlayerPrefs.SetFloat(file, value);
    }
    public static void Save(string file, int value)
    {
        PlayerPrefs.SetInt(file, value);
    }
    public static void Save(string file, string value)
    {
        PlayerPrefs.SetString(file, value);
    }

    public static int LoadInt(string file)
    {
        return PlayerPrefs.GetInt(file);
    }

    public static float LoadFloat(string file)
    {
        return PlayerPrefs.GetFloat(file);
    }

    public static string LoadString(string file)
    {
        return PlayerPrefs.GetString(file);
    }
}
