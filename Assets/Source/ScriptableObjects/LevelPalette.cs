using UnityEngine;

[CreateAssetMenu(menuName = "SO/Create new/Level palette")]
public class LevelPalette : ScriptableObject
{
    [SerializeField] private Material _skybox;
    [SerializeField] private Material _borderMaterial;
    [SerializeField] private Material _layoutMaterial;
    [SerializeField] private Material _backgroundMaterial;

    public Material Skybox => _skybox;
    public Material BorderMaterial => _borderMaterial;
    public Material LayoutMaterial => _layoutMaterial;
    public Material BackgroundMaterial => _backgroundMaterial;
}
