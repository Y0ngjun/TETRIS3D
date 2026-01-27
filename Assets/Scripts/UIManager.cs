using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // I, O, Z, S, J, L, T
    public Image nextBlockUI;
    public Sprite[] minos;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI highScore;
    public GameObject gameoverUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateNextBlockUI()
    {
        nextBlockUI.sprite = minos[GameManager.Instance.MinoSpawner.NextBlockIndex];
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "score: " + GameManager.Instance.Score;
    }

    public void UpdateLevelUI()
    {
        levelText.text = "level: " + GameManager.Instance.Level;
    }

    public void OnGameoverUI()
    {
        highScore.text = "high score: " + GameManager.Instance.HighScore;
        yourScore.text = "your score: " + GameManager.Instance.Score;
        gameoverUI.SetActive(true);
    }
}
