using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Text manaPointText = null;
    //public int maxManaPoint = 0;
    private float curManaPoint = 0;

    public float CurentManaPoint
    {
        get { return curManaPoint; }
        set { curManaPoint = value; }
    }

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        CurentManaPoint = masterData.MaxManaPoint;
    }

    // Update is called once per frame
    void Update()
    {
        CurentManaPoint += Time.deltaTime / 3;

        manaPointText.text = ((int)(curManaPoint)).ToString();

        if (CurentManaPoint >= masterData.MaxManaPoint) CurentManaPoint = masterData.MaxManaPoint;

    }
}
