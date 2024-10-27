using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Color _startColor = Color.white;

    public void SetRandomColor(Material material)
    {
        material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void SetStartColor(Material material)
    {
        material.color = _startColor;
    }
}