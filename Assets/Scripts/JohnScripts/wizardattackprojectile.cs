using UnityEngine;

public class WizardAttackProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Player player;
    private Vector2 target;

    void Start()
    {
        player = FindFirstObjectByType<Player>();

        target = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}