using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : LevelLayoutPart
{
    private Bounds _bounds;

    public void SetObject(GameObject gameObject)
    {
        Vector3 center = _bounds.center;
        gameObject.transform.position = new Vector3(center.x, center.y + _bounds.extents.z, center.z);
    }
    
    protected override Vector3 GetNewLocalScale(float targetHeight, float targetWidth)
    {
        return new Vector3(targetHeight, targetWidth, 1);
    }

    protected override Vector3 GetBoundsSize()
    {
        _bounds = GetComponent<BoxCollider>().bounds;
        return _bounds.size;
    }

    protected override void Initialize()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}
