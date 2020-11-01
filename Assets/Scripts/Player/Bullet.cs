using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifeSpan = 5000f;
    public float bulletDamage = 5f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthSystem>().DealDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
