using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPickup : MonoBehaviour
{
    public float healValue = 5f;
    public GameObject particles;
    public AudioClip drinkSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            HealthSystem hpSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (hpSystem.currentHP != hpSystem.maxHP)
            {
                hpSystem.Heal(healValue);
                Instantiate(particles, GameObject.FindGameObjectWithTag("Player").transform);
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(drinkSound);
                Destroy(gameObject);
            }
        }
    }
}
