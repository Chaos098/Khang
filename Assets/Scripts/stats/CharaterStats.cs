using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;
    private Rigidbody rb;

    [Header("Major stats")]
    public int strength; // 1 point increase damage by 1 and crit.power by 1%
    public int agility;  // 1 point increase evasion by 1% and crit.chance by 1%
    public int intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public int evasion;
    public int vitality; // 1 point incredase health by 5 points

    [Header("Offensive ints")]
    public int damage;
    public int critChance;
    public int critPower;              // default value 150%

    [Header("Defensive ints")]
    public int maxHealth;
    public int armor;

    public int currentHealth;

    public System.Action onHealthChanged;
    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }

    protected virtual void Start()
    {
        critPower = 150;
        currentHealth = GetMaxHealthValue();

        fx = GetComponent<EntityFX>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {

    }




    public virtual void TakeDamage(CharacterStats stats, int _damage)
    {

        if (isInvincible)
            return;

        if (currentHealth < maxHealth / 2)
        {
            _damage = CheckTargetArmor(stats, _damage);
            DecreaseHealthBy(_damage);
        }
        else
        {
            DecreaseHealthBy(_damage);
        }

        GetComponent<Entity>().DamageImpact();
        _ = fx.StartCoroutine("FlashFX");

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }


    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }


    protected virtual void DecreaseHealthBy(int _damage)
    {

        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
        isDead = true;
    }


    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;


    #region Stat calculations
    protected int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor;


        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }



    public int GetMaxHealthValue()
    {
        return maxHealth + vitality * 5;
    }

    #endregion

}