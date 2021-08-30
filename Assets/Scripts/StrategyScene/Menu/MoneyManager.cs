using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private Text moneyText = null;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        moneyText.text = masterData.GameMoney.ToString() + " G";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
