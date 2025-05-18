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
            GameManager.Instance.ShowEndGame(false);
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
        bool foundMatch = true;

        while (foundMatch)
        {
            foundMatch = false;

            if (current.Count < 3)
                return;

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
                    for (int i = 0; i < 3; i++)
                    {
                        Destroy(group.Value[i].gameObject);
                        current.Remove(group.Value[i]);
                    }

                    Rearrange();
                    foundMatch = true;
                    break;
                }
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
