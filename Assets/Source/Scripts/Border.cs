using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Border : MonoBehaviour
{
    private Vector3 _boundsSize;

    private void Awake()
    {
        _boundsSize = GetComponent<BoxCollider>().bounds.size;
    }

    public void Scale(float targetHeight, float targetWidth)
    {
        transform.localScale = new Vector3(targetWidth / _boundsSize.x, targetHeight, 1);
    }
}
