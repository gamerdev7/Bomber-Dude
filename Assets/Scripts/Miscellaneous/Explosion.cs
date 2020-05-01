using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<IMortal>().Die();
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
