using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IDamagable
{
    void Damage(int damages, bool isCritical);
}
