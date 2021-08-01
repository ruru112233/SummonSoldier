using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Prms
{
    public int Hp { get { return hp; }  set { hp = value; } }

    public int At { get { return at; }  set { at = value; } }

    public int Df { get { return df; }  set { df = value; } }

    public int Speed { get { return speed; }  set { speed = value; } }



    // Start is called before the first frame update
    public override void Start()
    {

    }

    private void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
