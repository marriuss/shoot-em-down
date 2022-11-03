using UnityEngine;

public abstract class LevelLayoutPart : MonoBehaviour
{
    protected Vector3 BoundsSize { get; private set; }

    private void Awake()
    {
        BoundsSize = GetBoundsSize();
        Initialize();
    }

    public void Scale(float targetHeight, float targetWidth)
    {
        transform.localScale = GetNewLocalScale(targetHeight, targetWidth);
    }

    protected abstract void Initialize();

    protected abstract Vector3 GetBoundsSize();

    protected abstract Vector3 GetNewLocalScale(float targetHeight, float targetWidth);
}
