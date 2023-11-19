using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerupEffects/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
    public float amount;

    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<Movement>().moveSpeed += amount;
    }
}
