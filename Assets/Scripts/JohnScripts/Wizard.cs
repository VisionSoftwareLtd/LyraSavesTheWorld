//using UnityEngine;

//public class Wizard : MonoBehaviour
//{
//    [SerializeField] private float speed;
//    [SerializeField] private float stoppingDistance;
//    [SerializeField] private float retreatDistance;

//    private float timeBtwShots;
//    [SerializeField] private float startTimeBtwShots;

//    [SerializeField] private GameObject projectile;
//    [SerializeField] private Transform player;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        player = GameObject.Find("Player").transform;
//        timeBtwShots = startTimeBtwShots;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(Vector2.Distance(transform.position, player.position)>stoppingDistance)
//        {
//            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
//        }
//        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
//        {
//            transform.position = this.transform.position;
//        }
//        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
//        {
//            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
//        }

//        if(timeBtwShots <= 0)
//        {
//            Instantiate(projectile, transform.position, Quaternion.identity);
//            timeBtwShots = startTimeBtwShots;
//        }
//        else
//        {
//             timeBtwShots -= Time.deltaTime;
//        }
//    }
//}
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float retreatDistance = 3f;

    [Header("Attack")]
    [SerializeField] private float startTimeBtwShots = 1.5f;
    [SerializeField] private GameObject projectile;

    private float timeBtwShots;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = 0f; 
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        HandleMovement(distance);
        HandleShooting(distance);
    }

    void HandleMovement(float distance)
    {
        if (distance > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else if (distance < retreatDistance)
        {
            Vector2 dir = (transform.position - player.position).normalized;
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
    }

    void HandleShooting(float distance)
    {
        if (distance <= stoppingDistance && distance > retreatDistance)
        {
            if (timeBtwShots <= 0f)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
