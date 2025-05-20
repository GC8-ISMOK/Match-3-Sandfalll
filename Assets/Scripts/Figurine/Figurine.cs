using DG.Tweening;
using UnityEngine;

public class Figurine : MonoBehaviour
{
    public string shape;
    public string color;
    public string animal;

    public SpriteRenderer spriteRenderer; 
    public SpriteRenderer frameRenderer;
    public void Init(string shape, string color, string animal)
    {
        this.shape = shape;
        this.color = color;
        this.animal = animal;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FigurineLibrary.Instance.GetSprite(shape, animal);

        Transform frame = transform.Find("Frame");
        if (frame != null)
        {
            frameRenderer = frame.GetComponent<SpriteRenderer>();
            frameRenderer.sprite = FigurineLibrary.Instance.GetFrameSprite(shape);
            frameRenderer.color = GetColorFromName(color);
        }
        else
        {
            Debug.LogWarning("Frame not found under Figurine!");
        }
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
    private void OnMouseDown()
    {
        GetComponent<Collider2D>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic; 
            rb.simulated = false; 
        }

        transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack).OnComplete(() =>
        {
            GameManager.Instance.OnFigurineClicked(this);
            Destroy(gameObject);
        });
    }
}
