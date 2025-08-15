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
      stamina.Add(stamina.passiveValue * Time.deltaTime);
        Debug.Log(stamina.curValue);
    }

    public void Die()
    {
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool HasStamina(float staminaValue)
    {
        if (stamina.curValue - staminaValue < 0)
        {
            return false;
        }
        if(CharacterManager.Instance.Player.controller.isMove)stamina.Subtract(staminaValue);
        return true;
    }

    public bool HasStaminaForJump(float staminaValue)
    {
        if (stamina.curValue - staminaValue < 0)
        {
            return false;
        }
        stamina.Subtract(staminaValue);
        return true;
    }
}
