using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class DragObj : DragCommon, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    public override void Start()
    {
        base.Start();
    }

    public virtual void OnBeginDrag(PointerEventData data)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(parentTransform.parent, false);
        instancePanel.transform.SetSiblingIndex(SibilingIndex - 1);
        instancePanel.SetActive(true);
    }

    public virtual void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
        instancePanel.SetActive(false);
        transform.SetParent(parentTransform, false);
        transform.SetSiblingIndex(SibilingIndex);
        canvasGroup.blocksRaycasts = true;
    }

}
