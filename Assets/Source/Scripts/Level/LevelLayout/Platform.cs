using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : LevelLayoutPart
{
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private Transform _objectPlacementPosition;

    private const float BounceStrength = 10f;
    private const float BoostDelaySeconds = 0.5f;

    private float _centerX = 0;
    private float _elapsedTime;
    private Enemy _attachedEnemy;

    public void Initialize(Enemy enemyPrefab)
    {
        _attachedEnemy = Instantiate(enemyPrefab);
        PlaceEnemy();
    }

    public void PlaceEnemy()
    {
        _attachedEnemy.Translate(_objectPlacementPosition.position);
    }

    protected override Vector3 GetNewLocalScale(float targetHeight, float targetWidth)
    {
        return new Vector3(targetHeight, targetWidth, 1);
    }

    protected override Vector3 GetBoundsSize()
    {
        return GetComponent<BoxCollider>().bounds.size;
    }
    
    protected override void Initialize()
    {
        transform.rotation = _startRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Weapon _))
            _elapsedTime = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Weapon weapon))
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= BoostDelaySeconds)
            {
                Vector3 currentPosition = weapon.transform.position;
                Vector3 targetPosition = currentPosition;
                targetPosition.x = _centerX;
                collision.rigidbody.AddForce((targetPosition - currentPosition).normalized * BounceStrength, ForceMode.Acceleration);
            }
        }
    }
}
