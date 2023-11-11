using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerupEffects/FireRateBuff")]
public class FireRateBuff : PowerupEffect
{
    public float amount;

    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<Movement>().fireRate += amount;
    }
}
