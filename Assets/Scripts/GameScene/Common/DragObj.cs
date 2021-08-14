using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentTransform;

    public void OnBeginDrag(PointerEventData data)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        parentTransform = transform.parent;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        transform.SetParent(parentTransform);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
