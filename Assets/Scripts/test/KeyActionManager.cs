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

        // �J�[�\���̈ړ�
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ��
            Debug.Log("��");
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // ��
            Debug.Log("��");
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // �E
            Debug.Log("�E");
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // ��
            Debug.Log("��");
        }
    }
}
