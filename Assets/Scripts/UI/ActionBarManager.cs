using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarManager : MonoBehaviour
{
    public static ActionBarManager Instance;

    public Transform[] slots = new Transform[7];
    public GameObject figurineUIPrefab;

    private List<FigurineUI> current = new List<FigurineUI>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddFigurine(FigurineData data)
    {
        if (current.Count >= 7)
        {
            GameManager.Instance.LoseGame();
            return;
        }

        var uiObj = Instantiate(figurineUIPrefab, slots[current.Count].position, Quaternion.identity, slots[current.Count]);
        var figUI = uiObj.GetComponent<FigurineUI>();
        figUI.Setup(data);
        current.Add(figUI);

        CheckMatch();
    }

    private void CheckMatch()
    {
        if (current.Count < 3) return;

        for (int i = 0; i <= current.Count - 3; i++)
        {
            var a = current[i];
            var b = current[i + 1];
            var c = current[i + 2];

            if (a.Matches(b) && a.Matches(c))
            {
                Destroy(a.gameObject);
                Destroy(b.gameObject);
                Destroy(c.gameObject);

                current.RemoveAt(i + 2);
                current.RemoveAt(i + 1);
                current.RemoveAt(i);

                Rearrange();
                break;
            }
        }
    }

    private void Rearrange()
    {
        for (int i = 0; i < current.Count; i++)
        {
            current[i].transform.SetParent(slots[i]);
            current[i].transform.position = slots[i].position;
        }
    }

    public void ClearAll()
    {
        foreach (var fig in current)
            Destroy(fig.gameObject);
        current.Clear();
    }
}
