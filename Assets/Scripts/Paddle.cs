using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rigibody { get; private set; }
    public Vector2 direction { get; private set; }
    public float speed = 30f;
    public float maxBounceAngel = 75f;
    private void Awake()
    {
        this.rigibody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if (this.direction != Vector2.zero)
        {
            this.rigibody.AddForce(this.direction * this.speed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Vector3 paddlePosition=this.transform.position;
            Vector2 contactPoint=collision.GetContact(0).point;

            float offset=paddlePosition.x - contactPoint.x;
            float width=collision.otherCollider.bounds.size.x;

            float currentAngel=Vector2.SignedAngle(Vector2.up,ball.rigibody.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngel;
            float newAngle = Mathf.Clamp(currentAngel + bounceAngle,-maxBounceAngel,maxBounceAngel);
            Quaternion rotation= Quaternion.AngleAxis(newAngle,Vector3.forward);

            ball.rigibody.velocity=rotation*rotation*Vector2.up*ball.rigibody.velocity.magnitude;

        }
    }
    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f,this.transform.position.y);
        this.rigibody.velocity = Vector2.zero;
    }
}
