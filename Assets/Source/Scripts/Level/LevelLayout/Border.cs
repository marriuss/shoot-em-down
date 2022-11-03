using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Border : LevelLayoutPart
{
    protected override Vector3 GetBoundsSize()
    {
        return GetComponent<BoxCollider>().bounds.size;
    }

    protected override Vector3 GetNewLocalScale(float targetHeight, float targetWidth)
    {
        return new Vector3(targetWidth / BoundsSize.x, targetHeight, 1);
    }

    protected override void Initialize() { }
}
