using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Text manaPointText = null;
    public int maxManaPoint = 0;
    public float curManaPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        curManaPoint = maxManaPoint;
    }

    // Update is called once per frame
    void Update()
    {
        curManaPoint += Time.deltaTime / 3;

        manaPointText.text = ((int)(curManaPoint)).ToString();

        if (curManaPoint >= maxManaPoint) curManaPoint = maxManaPoint;

    }
}
