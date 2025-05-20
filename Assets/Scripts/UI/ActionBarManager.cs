using System.Collections.Generic;
using UnityEngine;

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
            GameManager.Instance.ShowEndGame(false);
            return;
        }

        var uiObj = Instantiate(figurineUIPrefab, slots[current.Count].position, Quaternion.identity, slots[current.Count]);
        var figUI = uiObj.GetComponent<FigurineUI>();
        figUI.Setup(data);
        current.Add(figUI);

        Invoke(nameof(CheckMatch), 0.15f);
    }

    private void CheckMatch()
    {
        if (current.Count < 3) return;

        var groups = new Dictionary<string, List<FigurineUI>>();

        foreach (var fig in current)
        {
            string key = fig.GetKey();
            if (!groups.ContainsKey(key))
                groups[key] = new List<FigurineUI>();

            groups[key].Add(fig);
        }

        foreach (var group in groups)
        {
            if (group.Value.Count >= 3)
            {
                List<FigurineUI> toRemove = group.Value.GetRange(0, 3);

                foreach (var fig in toRemove)
                {
                    fig.AnimateAndDestroy();
                }

                current.RemoveAll(f => toRemove.Contains(f));

                Invoke(nameof(Rearrange), 0.3f);

                Invoke(nameof(CheckMatch), 0.35f);
                return;
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
