using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCommon : MonoBehaviour
{
    public Transform parentTransform;

    private int monsterIndex = -1;
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

    protected MasterData md = null;

    protected Image monsterImage = null;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        SibilingIndex = transform.GetSiblingIndex();
        md = GameObject.FindWithTag("MasterData").GetComponent<MasterData>();
        monsterImage = GetComponent<Image>();
        parentTransform = transform.parent;
    }

}
