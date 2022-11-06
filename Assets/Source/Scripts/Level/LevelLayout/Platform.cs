using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : LevelLayoutPart
{
    [SerializeField] private Transform _objectPlacementPosition;

    private Bounds _bounds;

    public void SetObjectOnTop(GameObject gameObject)
    {
        Vector3 center = _bounds.center;
        gameObject.transform.position = _objectPlacementPosition.position;
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
