using UnityEngine;
using UnityEngine.UI;

public class FigurineUI : MonoBehaviour
{
    public FigurineData data;
    public Image image;

    public void Setup(FigurineData d)
    {
        data = d;
        image = GetComponent<Image>();
    }

    public bool Matches(FigurineUI other)
    {
        return data.Equals(other.data);
    }
}
