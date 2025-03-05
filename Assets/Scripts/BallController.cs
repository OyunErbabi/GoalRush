using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallSide { Left, Right}

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed = 3.25f;
    public bool isGoal = false;
    GameObject GoalPosObject;
    public BallSide ballSide;

    GameObject goalPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1, 1).normalized * speed;
    }

    private void FixedUpdate()
    {
        if(!GameController.Instance.GameOver)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
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
            goalPoint = new GameObject();
            goalPoint.transform.SetParent(collision.transform);
            goalPoint.transform.position = transform.position;
            GameController.Instance.AddGoalPoint(goalPoint);
            GoalPosObject = goalPoint;
            isGoal = true;

            GameController.Instance.Goal(gameObject,ballSide);
        }
    }

    private void Update()
    {
        if (isGoal)
        {
            transform.position = GoalPosObject.transform.position;
        }
    }

    public void ResetBall()
    {
        GameController.Instance.RemoveGoalPoints(goalPoint);
        Destroy(goalPoint);
        goalPoint = null;

        isGoal = false;
        switch (ballSide)
        {
            case BallSide.Left:
                transform.position = new Vector2(-1f, 0);
                break;
            case BallSide.Right:
                transform.position = new Vector2(1f, 0);
                break;
        }
        rb.simulated = true;
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = randomDirection * speed;

    }

   

}
