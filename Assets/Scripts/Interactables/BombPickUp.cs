
using UnityEngine;

public class BombPickUp : MonoBehaviour, IPickUp
{
    public void Use()
    {
        if (GameManager.Instance.playerReference)
        {
            GameManager.Instance.playerReference.GetComponent<BombSpawner>().IncrementBombCapacity();
            Destroy(gameObject); 
        }
    }
}
