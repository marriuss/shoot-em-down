using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Money : ResetableMonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.TryGetComponent(out Weapon _) || collider.TryGetComponent(out Bullet _))
            ChangeAppearance(false);
    }

    public override void SetStartState()
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
