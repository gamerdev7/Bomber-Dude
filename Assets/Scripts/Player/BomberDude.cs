using UnityEngine;

public class BomberDude : MonoBehaviour, IMortal
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AudioClip walkSound;

    // Component References
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private BombSpawner bombSpawner;
    private LevelLoader levelLoader;

    private Vector2 movement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        bombSpawner = GetComponent<BombSpawner>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Normalize();


        if (movement != Vector2.zero)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            
            //play walk sound
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSound;
                audioSource.Play();
            }
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Die()
    {
        anim.SetTrigger("IsDead");
        
        this.enabled = false;
        bombSpawner.enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

        AudioManager.Instance.PlaySFX("PlayerDeath");
        GameManager.Instance.IsPlayerDead = true;

        Destroy(gameObject, 1f);

    }
}
