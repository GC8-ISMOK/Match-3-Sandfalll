using UnityEngine;

public class Figurine : MonoBehaviour
{
    public string shape;
    public string color;
    public string animal;

    public SpriteRenderer spriteRenderer;

    public void Init(string shape, string color, string animal)
    {
        this.shape = shape;
        this.color = color;
        this.animal = animal;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FigurineLibrary.Instance.GetSprite(shape, animal);
        spriteRenderer.color = GetColorFromName(color);
    }

    private Color GetColorFromName(string color)
    {
        switch (color)
        {
            case "red": return Color.red;
            case "blue": return Color.blue;
            case "green": return Color.green;
            default: return Color.white;
        }
    }
}
