using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtleManager : MonoBehaviour
{
    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    // ‚¨‹à‚ÌˆêŽž•Û‘¶
    private int money = 0;

    public int Money { get { return money; } set { money = value; } }

    // ŒoŒ±’l‚ÌˆêŽž•Û‘¶
    private int exp = 0;
    public int Exp { get { return exp; } set { exp = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Money = 0;
        Exp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Money>>>>>:" + Money);
        Debug.Log("Exp>>>>>:" + Exp);
    }
}
