using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private Color _startColor = Color.white;

    public void SetRandomColor() =>
        _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    public void SetStartColor() =>
        _renderer.material.color = _startColor;

    private void Awake() =>
        _renderer = GetComponent<Renderer>();
}