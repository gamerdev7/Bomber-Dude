using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickUp pickUp = collision.gameObject.GetComponent<IPickUp>();

        if (pickUp != null)
        {
            pickUp.Use();
            AudioManager.Instance.PlaySFX("Pickup");
        }
    }
}
