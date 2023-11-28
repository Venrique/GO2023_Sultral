using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerupEffects/ShootBuff")]
public class ShootBuff : PowerupEffect
{
   public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<PlayerShoot>().upLeft = true;
        target.GetComponent<PlayerShoot>().upRight = true;
        target.GetComponent<PlayerShoot>().downLeft = true;
        target.GetComponent<PlayerShoot>().downRight = true;
    }
}
