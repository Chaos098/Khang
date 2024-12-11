using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStats
{
    private enemy enemy;

    protected override void Start()
    {

        base.Start();

        enemy = GetComponent<enemy>();
    }

    public override void TakeDamage(CharacterStats stats, int _damage)
    {
        base.TakeDamage(stats, _damage);
    }

    protected override void Die()
    {
        base.Die();

        enemy.Die();
        Destroy(gameObject, 5f);
    }
}
