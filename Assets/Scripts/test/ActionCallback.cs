using System;
using UnityEngine;
using UnityEngine.Events;

public class ActionCallback : MonoBehaviour
{
    public static UnityAction KeyUpdate = null;
    
    public static UnityAction OnLongClick;

    public static UnityAction<Vector2> OnSwipe;

    public static UnityAction<int, int> OnChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyUpdate != null)
            KeyUpdate();

        //if (OnLongClick != null)
        //    OnLongClick();

        //if (OnSwipe != null)
        //    OnSwipe(Vector2.up);

        //if (OnChange != null)
        //    OnChange(1, 2);
        
    }
}
