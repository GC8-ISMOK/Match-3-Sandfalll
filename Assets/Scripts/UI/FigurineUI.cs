using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FigurineUI : MonoBehaviour
{
    [Header("UI References")]
    public Image figureImage; 
    public Image frameImage; 

    [HideInInspector]
    public FigurineData data;

    public void Setup(FigurineData data)
    {
        this.data = data;

        figureImage.sprite = FigurineLibrary.Instance.GetSprite(data.shape, data.animal);
        figureImage.color = Color.white;

        frameImage.sprite = FigurineLibrary.Instance.GetFrameSprite(data.shape);
        frameImage.color = GetColorByName(data.color);

        transform.localScale = Vector3.zero;
        transform.DOScale(0.6f, 0.3f).SetEase(Ease.OutBack);

        frameImage.DOFade(0.5f, 0.15f).SetLoops(2, LoopType.Yoyo);
    }


    public bool Matches(FigurineUI other)
    {
        return data.shape == other.data.shape &&
               data.color == other.data.color &&
               data.animal == other.data.animal;
    }

    public string GetKey()
    {
        return $"{data.shape}_{data.color}_{data.animal}";
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
    public void AnimateAndDestroy()
    {
        AudioManager.Instance.Play("3ElementsCollected");
        transform.DOScale(Vector3.zero, 0.25f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
