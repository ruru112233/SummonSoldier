using UnityEngine;

public class Spin : MonoBehaviour {
    private float turnDegPerSec = 30;
    public Vector3 localAxis;
    protected float turnDegPerFrame;

    public void Start()
    {
        turnDegPerFrame = turnDegPerSec;
    }

    public void Update()
    {
        transform.Rotate(localAxis, turnDegPerFrame * Time.deltaTime);
    }

}
