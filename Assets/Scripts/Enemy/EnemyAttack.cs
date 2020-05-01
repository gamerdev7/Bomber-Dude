using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<IMortal>().Die();
        }
    }
}
