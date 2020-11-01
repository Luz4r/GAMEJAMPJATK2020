using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHP = 100f;
    public GameObject gameOver;

    private HealthBar healthBar;
    private AudioSource aSource;
    private HurtSounds hSounds;
    private int hurtSoundIndex;

    [HideInInspector]
    public float currentHP;
    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        currentHP = maxHP;
        if (CompareTag("Player"))
        {
            aSource = GetComponent<AudioSource>();
            hSounds = GetComponent<HurtSounds>();
            hurtSoundIndex = 0;
        }
    }

    public void DealDamage(float dmg)
    {
        if (CompareTag("Player"))
        {
            aSource.PlayOneShot(hSounds.hurtClips[hurtSoundIndex++]);
            if (hurtSoundIndex >= hSounds.hurtClips.Length)
            {
                hurtSoundIndex = 0;
            }
        }
        if (dmg < currentHP)
        {
            currentHP -= dmg;
            healthBar.SetSize(currentHP / maxHP);
        }
        else
        {
            healthBar.SetSize(0f);
            Dead();
        }
        if(currentHP <= maxHP/3)
        {
            healthBar.SetColor();
        }
    }

    public void Heal(float healValue)
    {
        if(currentHP < maxHP)
        {
            float missingHP = maxHP - currentHP;
            if(missingHP <= healValue)
            {
                currentHP = maxHP;
                healthBar.SetSize(maxHP / maxHP);
            }
            else
            {
                currentHP += healValue;
                healthBar.SetSize(currentHP / maxHP);
            }
        }
    }

    public void Dead()
    {
        if(!gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            gameOver.SetActive(true);
            Time.timeScale = 0f;
            //print(Time.timeScale);
        }
    }
}
