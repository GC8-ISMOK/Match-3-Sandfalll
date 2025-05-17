using UnityEngine;

public class Figurine : MonoBehaviour
{
    public string shape;
    public string color;
    public string animal;

    public void Init(string shape, string color, string animal)
    {
        this.shape = shape;
        this.color = color;
        this.animal = animal;
    }

    private void OnMouseDown()
    {
        GameManager.Instance.OnFigurineClicked(this);
    }
}
