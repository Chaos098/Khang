using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : CharacterStats
{
    private Boss boss;

    protected override void Start()
    {

        base.Start();

        boss = GetComponent<Boss>();
    }

    public override void TakeDamage(CharacterStats stats, int _damage)
    {
        base.TakeDamage(stats, _damage);
    }

    protected override void Die()
    {
        base.Die();

        boss.Die();
        Destroy(gameObject, 5f);
    }
}
