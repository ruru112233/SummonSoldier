using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMonsterList : MonoBehaviour
{
    public int[] selectMonsterList = new int[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            selectMonsterList[i] = -1;
        }
    }

}
