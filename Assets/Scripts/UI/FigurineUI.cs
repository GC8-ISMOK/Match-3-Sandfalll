using UnityEngine;
using UnityEngine.UI;

public class FigurineUI : MonoBehaviour
{
    public Image image;
    public FigurineData data;

    public void Setup(FigurineData data)
    {
        this.data = data;

        Sprite sprite = FigurineLibrary.Instance.GetSprite(data.shape, data.animal);
        image.sprite = sprite;

        image.color = GetColorByName(data.color);
    }

    public bool Matches(FigurineUI other)
    {
        return data.shape == other.data.shape &&
               data.color == other.data.color &&
               data.animal == other.data.animal;
    }

    public string GetKey()
    {
        return data.shape + "_" + data.color + "_" + data.animal;
    }

    private Color GetColorByName(string name)
    {
        switch (name.ToLower())
        {
            case "red": return Color.red;
            case "blue": return Color.blue;
            case "green": return Color.green;
            default: return Color.white;
        }
    }
}
