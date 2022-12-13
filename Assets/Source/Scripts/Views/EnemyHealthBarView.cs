using UnityEngine;

public class EnemyHealthBarView : BarView
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.Health.ValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _enemy.Health.ValueChanged += OnHealthValueChanged;
    }

    private void Start()
    {
        SetSlider(_enemy.Health.MinValue, _enemy.Health.MaxValue);
        ChangeValue(_enemy.Health.CurrentValue);
    }

    private void Update()
    {
        if (_enemy.IsKnockedOut)
            Disable();
        else
            Enable();
    }

    private void OnHealthValueChanged(int newValue)
    {
        ChangeValue(newValue);
    }
}
