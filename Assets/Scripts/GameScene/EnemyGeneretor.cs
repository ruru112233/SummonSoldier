using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsterPrefabs = null;

    [SerializeField]
    private SummonPanelList enemyPanels = null;

    // ��������
    float intervalTime = 3.0f;
    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;

        if (startTime > intervalTime)
        {
            Debug.Log("�����X�^�[�̐���");
            MonsterInstance();
            startTime = 0;
        }
    }

    // �����X�^�[�̐���
    void MonsterInstance()
    {
        // �G�̃p�l�����擾����
        GameObject panel = ChildCheck.RandPanel(enemyPanels.panel);

        if (panel != null && monsterPrefabs.Count != 0)
        {
            EnemyInstance(panel);
        }

    }

    void EnemyInstance(GameObject panelObj)
    {
        Transform transform = panelObj.transform;

        int idx = Random.Range(0, monsterPrefabs.Count);

        // �������̈ʒu��ݒ�
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        // �v���n�u����C���X�^���X�𐶐�
        GameObject obj = (GameObject)Instantiate(monsterPrefabs[idx], pos, Quaternion.identity);

        // ���������I�u�W�F�N�g���q�Ƃ��ēo�^
        obj.transform.parent = panelObj.transform;
    }
}
