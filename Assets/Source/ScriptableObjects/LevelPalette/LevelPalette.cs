using UnityEngine;

[CreateAssetMenu(menuName = "SO/Create new/Level palette")]
public class LevelPalette : ScriptableObject
{
    [SerializeField] private Sprite _spaceBackground;
    [SerializeField] private Material _platformMaterial;
    [SerializeField] private Material _borderMaterial;
    [SerializeField] private Material _levelExitMaterial;

    public Sprite SpaceBackground => _spaceBackground;
    public Material PlatformMaterial => _platformMaterial;
    public Material BorderMaterial => _borderMaterial;
    public Material LevelExitMaterial => _levelExitMaterial;
}
