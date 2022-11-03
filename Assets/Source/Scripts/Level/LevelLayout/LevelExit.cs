using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshFilter))]
public class LevelExit : LevelLayoutPart
{
    public UnityAction PlayerFinishedLevel;

    protected override Vector3 GetBoundsSize()
    {
        return GetComponent<MeshFilter>().mesh.bounds.size;
    }

    protected override Vector3 GetNewLocalScale(float targetHeight, float targetWidth)
    {
        return new Vector3(targetHeight / BoundsSize.x, -1, targetWidth / BoundsSize.z);
    }

    protected override void Initialize() 
    {
        transform.rotation = Quaternion.Euler(0, 90, 90);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon _))
            PlayerFinishedLevel?.Invoke();
    }
}
