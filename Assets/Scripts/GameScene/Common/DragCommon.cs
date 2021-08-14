using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCommon : MonoBehaviour
{
    public Transform parentTransform;
    
    [SerializeField]
    protected GameObject instancePanel;

    protected CanvasGroup canvasGroup = null;
    protected int siblingIndex = 0;

    // Start is called before the first frame update
    public virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        siblingIndex = transform.GetSiblingIndex();
        parentTransform = transform.parent;
        Debug.Log(siblingIndex);
    }

}
