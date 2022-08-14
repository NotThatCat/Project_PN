using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATTACK_STATUS
{
    STOPING = 0,
    READY_ATTACK = 1,
    IN_DELAY = 2,
    ATTACKING = 3,
    ATTACKED = 4,
    IN_COOLDOWN = 5,
    DISABLE = 6,
}
