using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public List<int> hpList = null;
    public List<int> atList = null;
    public List<int> dfList = null;
    public List<int> speedList = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddStatusList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AddStatusList()
    {

        yield return new WaitUntil(() => MasterData.instance.playerUnitFlag);

        for (int i = 0; i < MasterData.instance.playerUnitList.Count; i++)
        {
            hpList.Add(0);
            atList.Add(0);
            dfList.Add(0);
            speedList.Add(0);
        }
    }
}
