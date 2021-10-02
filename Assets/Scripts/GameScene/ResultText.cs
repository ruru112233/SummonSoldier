using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ResultText : MonoBehaviour
{

    public Action OnComplete = null; // 終わった時のコールバック

    private Text text;
    private float speed;
    private float number;
    private float targetNumber;

    private Coroutine playCoroutine = null;

#if UNITY_EDITOR

    //[SerializeField]
    //private int debugToNumber = 10;
    //[SerializeField]
    //private float debugDuration = 1.0f;

#endif

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetNumber(int n)
    {
        number = (float)n;
        text.text = ((int)number).ToString();
        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
            playCoroutine = null;
        }
    }

    public void SlideToNumber(int from_number, int to_number, float duration)
    {
        SetNumber(from_number);
        SlideToNumber(to_number, duration);
    }
    

    public void SlideToNumber(int to_number, float duration)
    {
        targetNumber = to_number;
        speed = ((targetNumber - number) / duration);

        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine("slideTo");

    }



    private IEnumerator slideTo()
    {
        while (true)
        {
            var delta = speed * Time.deltaTime;
            var next_number = number + delta;
            text.text = ((int)next_number).ToString();

            number = next_number;

            if (UnityEngine.Mathf.Sign(speed) * (targetNumber - number) <= 0.0f)
            {
                break;
            }
            yield return null;
        }

        playCoroutine = null;
        number = targetNumber;
        text.text = ((int)number).ToString();
        if (OnComplete != null)
        {
            OnComplete();
            OnComplete = null;
        }
    }

    // お尻に文字がある場合
    public void SlideToNumber(int from_number, int to_number, float duration, string str)
    {
        SetNumber(from_number);
        SlideToNumber(to_number, duration, str);
    }

    public void SlideToNumber(int to_number, float duration, string str)
    {
        targetNumber = to_number;
        speed = ((targetNumber - number) / duration);

        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine(slideTo(str));

    }

    private IEnumerator slideTo(string str)
    {
        while (true)
        {
            var delta = speed * Time.deltaTime;
            var next_number = number + delta;
            text.text = ((int)next_number).ToString() + str;

            number = next_number;

            if (UnityEngine.Mathf.Sign(speed) * (targetNumber - number) <= 0.0f)
            {
                break;
            }
            yield return null;
        }

        playCoroutine = null;
        number = targetNumber;
        text.text = ((int)number).ToString() + str;
        if (OnComplete != null)
        {
            OnComplete();
            OnComplete = null;
        }
    }

}
