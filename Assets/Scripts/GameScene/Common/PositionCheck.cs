using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionCheck
{
    public static void PositionChenge(Transform transform, float correction)
    {
        string str = transform.parent.name;
        int len = str.Length;
        string s = str.Substring(len - 1);

        // ���i�ŏ�i�ɃI�u�W�F�N�g��������΁A��i�ɏグ��
        if (s.Equals("4") || s.Equals("5") || s.Equals("6"))
        {
            int idx = int.Parse(s) - 4;
            Transform chengeParent = ChildCheck.AvantGardeCheck(idx, transform);

            if (chengeParent != null) 
            {
                transform.parent = chengeParent.transform;
                transform.position = new Vector3(transform.position.x, transform.position.y, correction);
            }
        }
    }

}
