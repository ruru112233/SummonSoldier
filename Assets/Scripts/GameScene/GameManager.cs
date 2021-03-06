using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ManaManager manaManager = null;
    public SummonManager summonManager = null;

    public OnClick onClick = null;

    public LoadUnit loadUnit = null;
    public LoadBuyUnit loadBuyUnit = null;
    public LoadSelectMonster loadSelectMonster = null;

    public GameObject menuPanel = null;

    public static GameManager instance = null;

    public bool objCheck = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
