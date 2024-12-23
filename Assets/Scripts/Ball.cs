using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigibody { get; private set; }
    public float speed = 700f;
    public GameManager manager;
    private void Awake()
    {
        this.rigibody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        ResetBall();
    }
    private void SetRandomTrajectory() 
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        this.rigibody.AddForce(force.normalized * speed);
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigibody.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

}
