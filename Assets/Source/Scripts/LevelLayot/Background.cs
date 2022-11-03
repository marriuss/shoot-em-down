using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Background : LevelLayoutPart
{
    protected override Vector3 GetNewLocalScale(float targetHeight, float targetWidth)
    {
        return new Vector3(targetHeight / BoundsSize.x, -1, targetWidth / BoundsSize.z);
    }

    protected override Vector3 GetBoundsSize()
    {
        return GetComponent<MeshFilter>().mesh.bounds.size;
    }

    protected override void Initialize()
    {
        transform.rotation = Quaternion.Euler(0, 90, 90);
    }
}
