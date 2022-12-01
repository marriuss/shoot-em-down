using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class LevelLayoutPart : MonoBehaviour
{
    private Renderer _renderer;

    protected Vector3 BoundsSize { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        BoundsSize = GetBoundsSize();
        Initialize();
    }

    public void Scale(float targetHeight, float targetWidth)
    {
        transform.localScale = GetNewLocalScale(targetHeight, targetWidth);
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
    }

    protected abstract void Initialize();

    protected abstract Vector3 GetBoundsSize();

    protected abstract Vector3 GetNewLocalScale(float targetHeight, float targetWidth);
}
