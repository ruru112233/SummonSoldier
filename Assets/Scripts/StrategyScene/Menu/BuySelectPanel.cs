using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySelectPanel : MonoBehaviour
{
    [SerializeField]
    private Button yesButton = null,
                   noButton = null;

    public GameObject button;
    public int index;


    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;
        yesButton.onClick.SetListener(YesButton);
        noButton.onClick.SetListener(NoButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void YesButton()
    {
        Debug.Log("Yes");
        masterData.buyFlagList[index] = true;
        Destroy(button);
        Close();
    }

    void NoButton()
    {
        Debug.Log("No");
        Close();
    }

    void Close()
    {
        this.gameObject.SetActive(false);
    }
}
