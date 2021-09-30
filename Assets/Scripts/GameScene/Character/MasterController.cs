using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterController : Prms
{
    public int Hp { get { return hp; } set { hp = value; } }

    public int Df { get { return df; } set { df = value; } }

    ButtleManager buttleManager;

    [SerializeField]
    private int settingWinFlag;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        buttleManager = GameObject.FindWithTag("ButtleManager").GetComponent<ButtleManager>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (Hp <= 0)
        {
            buttleManager.WinFlag = settingWinFlag;
            Destroy(gameObject);
        }
    }

    public IEnumerator DamageText(int damage)
    {

        text.GetComponent<Text>().text = damage.ToString();

        text.transform.position = DamageTextPos(this.transform);

        text.SetActive(true);

        Hp -= damage;

        yield return new WaitForSeconds(0.5f);

        if (text != null)
        {
            text.SetActive(false);
        }

    }

    // テキストのポジションをセットする
    Vector3 DamageTextPos(Transform transform)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        return pos;
    }

}
