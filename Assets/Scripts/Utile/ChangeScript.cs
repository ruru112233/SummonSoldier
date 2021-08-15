using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeScript
{

    public static int SummonPanelIndex(string name)
    {
        int len = name.Length;
        string str = name.Substring(len - 1);
        int num = int.Parse(str) - 1;

        return num;
    }
}
