using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : LevelLayoutPart
{
    protected override Vector3 GetNewLocalScale(float targetHeight, float targetWidth)
    {
        return new Vector3(targetHeight, targetWidth, 1);
    }

    protected override Vector3 GetBoundsSize()
    {
        return GetComponent<BoxCollider>().bounds.size;
    }

    protected override void Initialize()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}
