using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }
    public void SlimeMovement(Vector3 mousePos,Vector3 center)
    {
        Vector3 distanceVec = mousePos - center;
        rb2d.velocity = -distanceVec*1.5f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Needle")) {
            GameLoop.Instance.GameOver();
        }
    }
}

