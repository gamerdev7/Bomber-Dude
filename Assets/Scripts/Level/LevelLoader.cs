using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

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
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public IEnumerator LoadLevel(string name, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(name);
    }

    public IEnumerator LoadNextLevel(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
