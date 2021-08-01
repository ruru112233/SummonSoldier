using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> summonButtons = null;

    [SerializeField]
    private SummonPanelList playerPanel = null;

    public List<GameObject> soldierPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SummonSoldier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SummonSoldier()
    {

        void AddSummonButton(int idx)
        {
            summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => SetPanel(idx));
        }

        for (int i = 0; i < summonButtons.Count; i++)
        {
            AddSummonButton(i);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setPanel"> �Z�b�g����p�l���i���o�[ </param>

    void SetPanel(int idx)
    {
        // �T�����p�l�����擾
        GameObject panelObj = playerPanel.panel[idx];
        
        Transform transform = panelObj.transform;

        // �������̈ʒu��ݒ�
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        // �T�����p�l�����\���W���[�����݂��Ă��Ȃ��ꍇ�A�\���W���[�𐶐�
        if (panelObj.transform.childCount == 0)
        {
            // �v���n�u����C���X�^���X�𐶐�
            GameObject obj = (GameObject)Instantiate(soldierPrefabs[idx], pos, Quaternion.identity);

            // ���������I�u�W�F�N�g���q�Ƃ��ēo�^
            obj.transform.parent = panelObj.transform;
        }
    }

}
