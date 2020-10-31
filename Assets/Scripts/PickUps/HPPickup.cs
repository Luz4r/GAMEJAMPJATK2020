using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPickup : MonoBehaviour
{
    public float healValue = 5f;
    public GameObject particles;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<HealthSystem>().Heal(healValue);
            Instantiate(particles, GameObject.FindGameObjectWithTag("Player").transform);
            Destroy(gameObject);
        }
    }
}
