using UnityEngine;

public class LimitZoneCheck : MonoBehaviour
{
    [SerializeField] private Material     _edgeMaterial;
    [SerializeField] private MeshRenderer _edgeMeshRenderer;

    [Header("Shader Reference")]
    [SerializeField] private string _protagonistPosition = "_ProtagonistPosition";

    private void Awake()
    {
        _edgeMaterial              = new Material(_edgeMaterial);
        _edgeMeshRenderer.material = _edgeMaterial;
    }

    private void Update()
    {
        if (Protagonist.Current != null)
        {
            _edgeMaterial.SetVector(_protagonistPosition, Protagonist.Current.transform.position);
        }
    }
}
