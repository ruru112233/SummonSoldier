using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class UnityEventExt
{

    //public static void SetListener(this Button.ButtonClickedEvent self, UnityAction call)
    //{
    //    self.RemoveAllListeners();
    //    self.AddListener( call );
    //}

    public static void SetListener(this UnityEvent self, UnityAction call)
    {
        self.RemoveAllListeners();
        self.AddListener(call);
    }

}
