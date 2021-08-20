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
            if (idx < 3)
            {
                // ��i�̃Z�b�g
                //summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => UpperRowSummon(idx));
            }
            else
            {
                // ���i�̃Z�b�g
                //summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => LowerRowSummon(idx));
            }
        }

        for (int i = 0; i < summonButtons.Count; i++)
        {
            AddSummonButton(i);
        }

    }

    public void UpperRowSummon(int idx, int panelNo)
    {
        // ��i�̃p�l�����擾
        GameObject[] upperPanels = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            upperPanels[i] = playerPanel.panel[i];
        }

        // �p�l���̎q�v�f�`�F�b�N
        GameObject panelObj = ChildCheck.PanelCheck2(upperPanels[panelNo]);

        // �p�l���̎q�v�f���󂾂�����A��̃p�l���ɃZ�b�g����B
        if (panelObj != null)
        {
            Summon(panelObj, idx);
        }
    }

    public void LowerRowSummon(int idx, int panelNo)
    {
        GameObject[] lowerPanels = new GameObject[3];

        for (int i = 3; i < 6; i++)
        {
            lowerPanels[i - 3] = playerPanel.panel[i];
        }

        // �p�l���̎q�v�f�`�F�b�N
        GameObject panelObj = ChildCheck.PanelCheck2(lowerPanels[panelNo]);

        // �p�l���̎q�v�f���󂾂�����A��̃p�l���ɃZ�b�g����B
        if (panelObj != null)
        {
            Summon(panelObj, idx);
        }
    }

    // ����
    public void Summon(GameObject panelObj, int idx)
    {
        Transform transform = panelObj.transform;

        // �������̈ʒu��ݒ�
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Debug.Log("idx>>>:" + idx);

        // �v���n�u����C���X�^���X�𐶐�
        GameObject obj = (GameObject)Instantiate(soldierPrefabs[idx], pos, Quaternion.identity);

        // ���������I�u�W�F�N�g���q�Ƃ��ēo�^
        obj.transform.parent = panelObj.transform;
    }


}
