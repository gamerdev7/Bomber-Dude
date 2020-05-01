using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private GameObject explosion;

    public int ExplosionLength { get; set; }

    private CircleCollider2D bodyCollider;

    private void Start()
    {
        bodyCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        explosionDelay -= Time.deltaTime;

        if (explosionDelay < 0)
        {
            FindObjectOfType<MapDestroyer>().Explode(transform.position, explosion, ExplosionLength);
            
            var bombSpawner = FindObjectOfType<BombSpawner>();
            if (bombSpawner)
            {
                bombSpawner.IncrementBombCount();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bodyCollider.isTrigger = false;
    }
}
