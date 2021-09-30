﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyHitbox : Collidable
{
    // Damage
    public int damage = 1;
    public float pushForce = 2;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            // Create new damage object, before sending to player.
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);
        }
    }
}
