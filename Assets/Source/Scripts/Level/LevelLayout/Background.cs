using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Background : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSize(float width, float height)
    {
        transform.localScale = new Vector2(width, height);
    }

    public void SetMaterial(Material material)
    {
        _spriteRenderer.material = material;
    }
}
