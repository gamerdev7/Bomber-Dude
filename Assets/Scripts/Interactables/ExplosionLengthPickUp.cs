using UnityEngine;

public class ExplosionLengthPickUp : MonoBehaviour, IPickUp
{
    public void Use()
    {
        if (GameManager.Instance.playerReference)
        {
            GameManager.Instance.playerReference.GetComponent<BombSpawner>().IncrementExplosionLength();
            Destroy(gameObject);
        }
    }
}
