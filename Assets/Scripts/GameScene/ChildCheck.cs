using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChildCheck
{
 
    public static GameObject PanelCheck(GameObject[] panels)
    {

        foreach (GameObject obj in panels)
        {
            if (obj.transform.childCount == 0)
            {
                return obj;
            }
        }

        return null;
    }

    public static GameObject RandPanel(List<GameObject> panels)
    {
        List<GameObject> newPanels = new List<GameObject>();
        newPanels.Clear();

        foreach (GameObject obj in panels)
        {
            if (obj.transform.childCount == 0)
            {
                newPanels.Add(obj);
            }
        }

        if(newPanels.Count != 0)
        {
            int randNum = Random.Range(0, newPanels.Count);
            return newPanels[randNum];
        }

        return null;
    }
}
