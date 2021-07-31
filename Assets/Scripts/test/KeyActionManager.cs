using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActionManager : MonoBehaviour
{
    private void Awake()
    {
        ActionCallback.KeyUpdate = KeyAction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KeyAction()
    {

        // カーソルの移動
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 上
            Debug.Log("上");
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 下
            Debug.Log("下");
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 右
            Debug.Log("右");
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 左
            Debug.Log("左");
        }
    }
}
