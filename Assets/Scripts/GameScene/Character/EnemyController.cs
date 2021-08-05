using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class EnemyController : Prms
{
    [SerializeField]
    private GameObject text = null;

    private Vector3 startTextPos = new Vector3();

    public int Hp { get { return hp; } set { hp = value; } }

    public int At { get { return at; } set { at = value; } }

    public int Df { get { return df; } set { df = value; } }

    public int Speed { get { return speed; } set { speed = value; } }

    int hp1 = 0;
    int hp2 = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        hp1 = Hp;
        hp2 = Hp;

        startTextPos = text.transform.position;
        startTextPos.y += 1.0f;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //hpText.text = Hp.ToString();

        if (OnDamegeCheck(Hp))
        {
            Debug.Log("ダメージ");

        }

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // ダメージ判定
    bool OnDamegeCheck(int hp)
    {
        if (Time.frameCount % 2 == 0)
        {
            hp1 = Hp;
        }
        else
        {
            hp2 = Hp;
        }

        if (hp1 != hp2)
        {
            return true;
        }

        return false;
    }

    public void setDamage(int damage)
    {
        this.Hp -= damage;
    }

    public async void DamageText(int damage)
    {
        text.SetActive(true);

        text.GetComponent<Text>().text = damage.ToString();
        text.transform.position = startTextPos;

        Hp -= damage;

        await Task.Delay(800);

        if (text != null)
        {
            text.SetActive(false);
        }
        
    }
}
