using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    private Transform player; // Reference to the player object
    private bool onDamaged, isPlayerDetected = false, isDead = false; // Flag to check if the player is detected

    GameObject dustShootEffect;
    public GameObject gun;
    public GameObject bullet;
    public GameObject dustShoot;

    private Rigidbody2D rb;
    private Animator anim;
    public GameObject groundCheck;
    public LayerMask groundLayer;

    public Transform startBullet;

    public float hp = 400;
    public float speed;
    public float circleRadius;
    public float shootingRange;

    public bool facingRight;
    public bool isGrounded;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        timer += Time.deltaTime;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        // Automatically move
        autoMoving();

        // Detected player
        if (distanceFromPlayer <= shootingRange)
        {
            isPlayerDetected = true;
        }
        else isPlayerDetected = false;

        if (isPlayerDetected)
        {
            detectedAnimation();
        }
        else
        {
            if (facingRight)
                gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else
                gun.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
            (gun.GetComponent<EnemyGunRotate>() as MonoBehaviour).enabled = false;
        }


        // Animation when get hurt
        if (onDamaged)
        {
            anim.SetBool("isHurt", true);
            StartCoroutine(UnDamaged());
        }

        if (!onDamaged)
        {
            anim.SetBool("isHurt", false);
        }

        // Animation when dead
        if (isDead)
        {
            StartCoroutine(DeadAnimation(1.5f));
        }

    }

    private void autoMoving()
    {
        anim.SetBool("isMoving", true);
        rb.velocity = Vector2.right * speed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        if (!isGrounded && facingRight)
        {
            Flip();
        }
        else if (!isGrounded && !facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        gameObject.transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }



    public void OnDamaged(float Damage)
    {
        onDamaged = true;
        hp -= Damage;
        if (hp <= 0)
        {
            isDead = true;
        }
    }

    private void detectedAnimation()
    {
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("isMoving", false);

        (gun.GetComponent<EnemyGunRotate>() as MonoBehaviour).enabled = true;

        if (Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(player.transform.position)).x < transform.position.x && facingRight)
            Flip();
        else if (Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(player.transform.position)).x > transform.position.x && !facingRight)
            Flip();

        if (timer > 1f)
        {
            timer = 0;
            shooting();
            dustShootEffect = Instantiate(dustShoot, gun.transform.GetChild(2).transform.position, gun.transform.GetChild(2).transform.rotation);
            Destroy(dustShootEffect, 1f);
        }

        Destroy(dustShootEffect, 1f);
    }

    void shooting()
    {
        GameObject shoot = Instantiate(bullet, gun.transform.GetChild(0).position, gun.transform.GetChild(0).rotation);
        Destroy(shoot, 3f);
    }

    IEnumerator DeadAnimation(float seconds)
    {
        anim.SetBool("isDead", true);
        gun.SetActive(false);
        isPlayerDetected = false;

        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);
    }

    IEnumerator UnDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        onDamaged = false;
    }



}












//Vector3 localScale = transform.localScale;

//// Moving from A -> B & B -> A
//if (currentPoint == pointB.transform)
//{
//    rb.velocity = new Vector2(speed, 0);
//}
//else
//{
//    rb.velocity = new Vector2(-speed, 0);
//}


//// Rotate when reach edge point
//if (Vector2.Distance(transform.position, currentPoint.position) < distance && currentPoint == pointB.transform)
//{
//    localScale.x *= -1;
//    transform.localScale = localScale;

//    currentPoint = pointA.transform;
//}

//if (Vector2.Distance(transform.position, currentPoint.position) < distance && currentPoint == pointA.transform)
//{
//    localScale.x *= -1;
//    transform.localScale = localScale;

//    currentPoint = pointB.transform;
//}



//// Back to movement line after being damaged
//if ((currentPoint == pointA.transform && localScale.x > 0) || (currentPoint == pointB.transform && localScale.x < 0))
//{
//    localScale.x *= -1;
//    transform.localScale = localScale;
//}
