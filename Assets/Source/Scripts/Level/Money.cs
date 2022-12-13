using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _rotationAnglePerSecond;

    private BoxCollider _boxCollider;
    private MeshRenderer[] _meshRenderers;

    public int Value => _value;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        transform.Rotate(_rotationAnglePerSecond * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Weapon _) || collider.TryGetComponent(out Bullet _))
            ChangeAppearance(false);
    }

    public void Appear()
    {
        ChangeAppearance(true);
    }

    private void ChangeAppearance(bool isVisible)
    {
        _boxCollider.enabled = isVisible;

        foreach (MeshRenderer meshRenderer in _meshRenderers)
            meshRenderer.enabled = isVisible;
    }
}
