using UnityEngine;

public class Background : MonoBehaviour
{
    public void SetSize(float width, float height)
    {
        transform.localScale = new Vector2(width, height);
    }
}
