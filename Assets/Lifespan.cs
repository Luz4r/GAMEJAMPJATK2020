using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{

    public float lifeSpan = 5000;
    float bornTime;
    float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime = Time.time - bornTime;

        if (lifetime > lifeSpan) {
            Destroy(gameObject);
        }
    }
}
