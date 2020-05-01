using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.EnemyCount <= 0)
            {
                StartCoroutine(LevelLoader.Instance.LoadNextLevel(1f));
            }
        }
    }
}
