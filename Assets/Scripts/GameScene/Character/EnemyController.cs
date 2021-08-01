using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Prms
{
    [SerializeField]
    private Text hpText = null;

    public int Hp { get { return hp; } set { hp = value; } }

    public int At { get { return at; } set { at = value; } }

    public int Df { get { return df; } set { df = value; } }

    public int Speed { get { return speed; } set { speed = value; } }

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = Hp.ToString();

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
