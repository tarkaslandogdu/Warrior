using UnityEngine;

public class playerDeath : MonoBehaviour
{
    private Rigidbody2D rD;
    private Animator anim;
    private Vector3 respawnPoint;

    [SerializeField] private AudioSource deathSound;

    void Start()
    {
        rD = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
            Die();
        }

        if (collision.tag == "Void")
        {
            transform.position=respawnPoint;
        }
        if (collision.tag == "Respawn")
        {
            respawnPoint = transform.position;
        }
    }
    private void Die()
    {
        deathSound.Play();
        rD.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }

    void Respawn()
    {
        transform.position = respawnPoint;
        rD.bodyType = RigidbodyType2D.Dynamic;
        anim.SetTrigger("Respawn");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

