using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsterPrefabs = null;

    [SerializeField]
    private SummonPanelList enemyPanels = null;

    ButtleManager buttleManager;

    // ��������
    float intervalTime = 6.0f;
    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        buttleManager = GameObject.FindWithTag("ButtleManager").GetComponent<ButtleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!buttleManager.EndFlag)
        {
            startTime += Time.deltaTime;

            if (startTime > intervalTime)
            {
                MonsterInstance();
                startTime = 0;
            }
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
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // �v���n�u����C���X�^���X�𐶐�
        GameObject obj = (GameObject)Instantiate(monsterPrefabs[idx], pos, Quaternion.identity);

        // ���������I�u�W�F�N�g���q�Ƃ��ēo�^
        obj.transform.parent = panelObj.transform;
    }
}
