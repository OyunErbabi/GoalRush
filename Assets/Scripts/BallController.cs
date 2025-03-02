using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed = 3.25f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1, 1).normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);

         
            float randomAngle = Random.Range(-35f, 35f);

         
            float angleRad = randomAngle * Mathf.Deg2Rad;

         
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);
            Vector2 newDirection = new Vector2(
                reflection.x * cos - reflection.y * sin,
                reflection.x * sin + reflection.y * cos
            );

         
            rb.velocity = newDirection.normalized * speed;
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
