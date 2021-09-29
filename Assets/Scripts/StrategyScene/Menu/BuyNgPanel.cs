using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyNgPanel : MonoBehaviour
{
    [SerializeField]
    private Button buyNgCloseButton;

    // Start is called before the first frame update
    void Start()
    {
        buyNgCloseButton.onClick.SetListener(CloseButton); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 閉じるボタン
    void CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
