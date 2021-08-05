using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_Camera : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvasRectTfm;
    [SerializeField]
    private Transform targetTfm;

    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 1.5f, 0);
    Vector2 pos;

    Vector2 pos2 = new Vector2(200, 500);

    // Start is called before the first frame update
    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTfm, screenPos, Camera.main, out pos);

        myRectTfm.position = pos + pos2;
    }
}
