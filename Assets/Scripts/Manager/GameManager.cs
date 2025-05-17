using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject figurinePrefab;

    public GameObject winScreen;
    public GameObject loseScreen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnFigurines(30);
    }

    public void SpawnFigurines(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 6f + i * 0.5f, 0f);
            var obj = Instantiate(figurinePrefab, spawnPos, Quaternion.identity);
            Figurine f = obj.GetComponent<Figurine>();

            f.Init("circle", "red", "cat");
        }
    }

    public void OnFigurineClicked(Figurine fig)
    {
        FigurineData data = new FigurineData(fig.shape, fig.color, fig.animal);

        Destroy(fig.gameObject);
        ActionBarManager.Instance.AddFigurine(data);

        CheckVictory();
    }

    public void CheckVictory()
    {
        if (GameObject.FindObjectsOfType<Figurine>().Length == 0)
        {
            winScreen.SetActive(true);
        }
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
    }
}
