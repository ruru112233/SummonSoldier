using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    [SerializeField]
    private Button summonButton = null;

    public List<GameObject> soldierPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        summonButton.onClick.SetListener(SummonSoldier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SummonSoldier()
    {
        SetPanel(1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setPanel"> �Z�b�g����p�l���i���o�[ </param>

    void SetPanel(int setPanel)
    {
        // �T�����p�l�����擾
        GameObject[] panelObj = GameObject.FindGameObjectsWithTag("panel1");

        // �������̈ʒu��ݒ�
        Vector3 pos = new Vector3(0, 0.1f, 0);

        // �T�����p�l�����\���W���[�����݂��Ă��Ȃ��ꍇ�A�\���W���[�𐶐�
        if (panelObj[setPanel - 1].transform.childCount == 0)
        {
            // �v���n�u����C���X�^���X�𐶐�
            GameObject obj = (GameObject)Instantiate(soldierPrefabs[0], pos, Quaternion.identity);

            // ���������I�u�W�F�N�g���q�Ƃ��ēo�^
            obj.transform.parent = panelObj[setPanel - 1].transform;
        }

    }

}
