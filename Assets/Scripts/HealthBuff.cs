using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerupEffects/HealthBuff")]
public class HealthBuff : PowerupEffect
{
    public int amount;

    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<HealthPlayer>().healthRegen += amount;
    }
}
