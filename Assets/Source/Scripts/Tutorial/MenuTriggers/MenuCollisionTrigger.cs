using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCollisionTrigger : MenuTrigger
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Weapon _))
            InvokeTrigger();
    }
}
