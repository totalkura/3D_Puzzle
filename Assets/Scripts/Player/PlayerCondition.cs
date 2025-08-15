using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//public interface IDamagable
//{
//    void TakePhysicalDamage(int damage);
//}

public class PlayerCondition : MonoBehaviour/*, IDamagable*/
{
    public UICondition uiCondition;


    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;

    void Update()
    {

        health.Subtract(health.passiveValue * Time.deltaTime);

        if (health.curValue == 0f)
        {
            Die();
        }

    }

    public void Die()
    {
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
