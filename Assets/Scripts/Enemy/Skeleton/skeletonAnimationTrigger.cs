using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonAnimationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    enemySkeleton enemy => GetComponentInParent<enemySkeleton>();
    public void triggerAnimation()
    {
        enemy.AnimationFinishTriger();
    }
    public void AttackTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<PlayerMove>() != null)
            {
                hit.GetComponent<PlayerMove>().OnDamaged(20);
            }
        }
    }
}
