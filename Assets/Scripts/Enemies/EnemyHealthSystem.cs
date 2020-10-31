using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public float maxHP = 100f;

    private EnemyHealthBar healthBar;
    private float currentHP;
    private void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
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
        }
    }
}
