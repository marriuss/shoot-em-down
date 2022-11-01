using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Background : MonoBehaviour
{
    private Vector3 _boundsSize;

    private void Awake()
    {
        _boundsSize = GetComponent<MeshFilter>().mesh.bounds.size;
    }

    public void Scale(float targetHeight, float targetWidth)
    {
        transform.localScale = new Vector3(targetHeight / _boundsSize.x, -1, targetWidth / _boundsSize.z);
    }
}
