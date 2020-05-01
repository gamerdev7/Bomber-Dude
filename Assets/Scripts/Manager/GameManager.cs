using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerReference;

    public bool IsPlayerDead = false;
    public int Score { get; set; }
    public int EnemyCount
    {
        get
        {
            return FindObjectsOfType<Enemy>().Length;
        }
    }

    public bool CanMakeEnemiesFast { get; set; } = false;
    private bool haveEnemiesSpeedIncreased = false;

    public int bombCapacity = 1;
    public int explosionLength = 1;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        CanMakeEnemiesFast = false;
        haveEnemiesSpeedIncreased = false;

        playerReference = GameObject.FindGameObjectWithTag("Player");

        if (scene.name == "GameOverScene")
        {
            AudioManager.Instance.PlayMusic("YouLose");
        }
        else if (scene.name == "WinScene")
        {
            AudioManager.Instance.PlayMusic("YouWin");
        }
        else if (scene.name == "StartMenuScene")
        {
            AudioManager.Instance.PlayMusic("Theme1");
            ResetPlayerData();
        }
    }

    private void Update()
    {
        if (!haveEnemiesSpeedIncreased && CanMakeEnemiesFast)
        {
            MakeEnemiesFast();
            haveEnemiesSpeedIncreased = true;
        }

        if (IsPlayerDead)
        {
            IsPlayerDead = false;
            StartCoroutine(LevelLoader.Instance.LoadLevel("GameOverScene", 1.3f));
        }
    }

    private void MakeEnemiesFast()
    {
        var allEnemies = FindObjectsOfType<Enemy>();

        if (allEnemies != null)
        {
            foreach (var enemy in allEnemies)
            {
                enemy.IncreaseMoveSpeed(4f);
            }
        }
    }

    private void ResetPlayerData()
    {
        Score = 0;
        bombCapacity = 1;
        explosionLength = 1;
    }
}
