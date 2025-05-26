using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : AbstractEnemy
{
    void Update()
    {

    }

    private void FixedUpdate()
    {
        base.MoveTowardsPlayer();

    }

    protected override void OnTakeDamage(float damage, EnemyBehaviour enemy)
    {
        if (enemy != this) return; // Обеспечить, что мы не принимаем урон от других врагов

        currentHealth -= damage;
       
        if (currentHealth <= 0)
        {
            OnGiveEXPAfterDeath();
            return;
        }
        DamageMarker(0.1f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision, damageAbillController, this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GravityOrb"))
        {
            isGravitised = false;
            Gravity.OnGravity -= Ongravitise;
            
        }
    }
    
}
