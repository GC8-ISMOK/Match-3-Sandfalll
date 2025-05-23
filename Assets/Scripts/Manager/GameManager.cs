using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public GameObject figurinePrefab;

    public GameObject winScreen;
    public GameObject loseScreen;

    public GameObject endGamePanel;
    public Text endGameText;
    public Button restartButton;

    public string[] shapes = { "circle", "square", "triangle" };
    public string[] colors = { "red", "blue", "green" };
    public string[] animals = { "cat", "dog", "fox" };


    public int currentFigurineCount;

    public ParticleSystem confettiR;
    public ParticleSystem confettiL;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        endGamePanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        SpawnFigurines(currentFigurineCount);
        AudioManager.Instance.Play("MusicMain");
    }

    public void SpawnFigurines(int count)
    {
        currentFigurineCount = count;
        int tripleCount = count / 3;
        List<FigurineData> pool = new List<FigurineData>();

        for (int i = 0; i < tripleCount; i++)
        { 
            string shape = shapes[Random.Range(0, shapes.Length)];
            string color = colors[Random.Range(0, colors.Length)];
            string animal = animals[Random.Range(0, animals.Length)];

            for (int j = 0; j < 3; j++)
            {
                pool.Add(new FigurineData(shape, color, animal));
            }
        }

        Shuffle(pool);

        for (int i = 0; i < pool.Count; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 6f + i * 0.5f, 0f);
            GameObject obj = Instantiate(figurinePrefab, spawnPos, Quaternion.identity);
            Figurine f = obj.GetComponent<Figurine>();
            f.Init(pool[i].shape, pool[i].color, pool[i].animal);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public void OnFigurineClicked(Figurine fig)
    {

        AudioManager.Instance.Play("FigurineDestroy");
        FigurineData data = new FigurineData(fig.shape, fig.color, fig.animal);

        Destroy(fig.gameObject);

        ActionBarManager.Instance.AddFigurine(data);

        StartCoroutine(DelayedVictoryCheck());
    }

    private IEnumerator DelayedVictoryCheck()
    {
        yield return new WaitForEndOfFrame(); 
        CheckVictory();
    }

    public void CheckVictory()
    {

        var figurinesLeft = GameObject.FindObjectsOfType<Figurine>();

        if (figurinesLeft.Length == 0)
        {
            AudioManager.Instance.Play("Applause");
            AudioManager.Instance.Play("Win");
            AudioManager.Instance.Stop("MusicMain");
            confettiR.Play();
            confettiL.Play();
            ShowEndGame(true); 
        }
    }

    public void ReshuffleField()
    {

        foreach (var fig in GameObject.FindObjectsOfType<Figurine>())
        {
            Destroy(fig.gameObject);
        }

        StartCoroutine(DelayedSpawn(currentFigurineCount));
    }

    private IEnumerator DelayedSpawn(int count)
    {
        yield return new WaitForSeconds(0.5f);
        SpawnFigurines(count);
    }

    public void ShowEndGame(bool isWin)
    {
        endGameText.text = isWin ? "You Win!" : "You Lose!";
        endGamePanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
