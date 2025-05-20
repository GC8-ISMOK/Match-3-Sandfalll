using UnityEngine;

public class FigurineLibrary : MonoBehaviour
{
    public static FigurineLibrary Instance;

    [Header("Sprite")]
    public Sprite circleCat;
    public Sprite squareCat;
    public Sprite triangleCat;

    public Sprite circleDog;
    public Sprite squareDog;
    public Sprite triangleDog;

    public Sprite circleFox;
    public Sprite squareFox;
    public Sprite triangleFox;

    [Header("Frame")]
    public Sprite squareFrame;
    public Sprite circleFrame;
    public Sprite triangleFrame;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Sprite GetSprite(string shape, string animal)
    {
        switch (shape + animal)
        {
            case "circlecat": return circleCat;
            case "squarecat": return squareCat;
            case "trianglecat": return triangleCat;

            case "circledog": return circleDog;
            case "squaredog": return squareDog;
            case "triangledog": return triangleDog;

            case "circlefox": return circleFox;
            case "squarefox": return squareFox;
            case "trianglefox": return triangleFox;

            default: return null;
        }
    }
    public Sprite GetFrameSprite(string shape)
    {
        switch (shape.ToLower())
        {
            case "square": return squareFrame;
            case "circle": return circleFrame;
            case "triangle": return triangleFrame;
            default: return squareFrame;
        }
    }
}
