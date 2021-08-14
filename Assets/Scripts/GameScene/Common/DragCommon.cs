using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCommon : MonoBehaviour
{
    public Transform parentTransform;

    private int monsterIndex = 0;
    private int siblingIndex = 0;

    public int MonsterIndex
    {
        get { return monsterIndex; }
        set { monsterIndex = value; }
    }

    public int SibilingIndex
    {
        get { return siblingIndex; }
        set { siblingIndex = value; }
    }

    
    [SerializeField]
    protected GameObject instancePanel;

    protected CanvasGroup canvasGroup = null;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        SibilingIndex = transform.GetSiblingIndex();
        parentTransform = transform.parent;
    }

}
