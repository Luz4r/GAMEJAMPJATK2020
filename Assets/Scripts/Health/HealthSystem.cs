﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHP = 100f;

    private HealthBar healthBar;
    private float currentHP;
    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        currentHP = maxHP;
    }

    public void DealDamage(float dmg)
    {
        if (currentHP > 0)
        {
            if (dmg <= maxHP)
            {
                currentHP -= dmg;
                healthBar.SetSize(currentHP / maxHP);
            }
            else
            {
                healthBar.SetSize(0f);
            }
            if(currentHP <= maxHP/3)
            {
                healthBar.SetColor();
            }
        }
        else
        {
            Dead();
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

    private void Dead()
    {
        if(!gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
        //TODO add dead screen
        //TOOD kill player
        }
    }
}
