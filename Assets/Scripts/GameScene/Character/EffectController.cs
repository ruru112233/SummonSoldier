using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    float time = 0;
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 1.5f)
        {
            time = 0;
            particle.Stop(true);
        }
    }
}
