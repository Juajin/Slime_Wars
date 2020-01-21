using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Character selectedCharacter;
    public bool isGetLifeBefore;
    private void Start()
    {
        isGetLifeBefore = false;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
    }
    public void SlimeMovement(Vector3 mousePos, Vector3 center)
    {
        Vector3 distanceVec = mousePos - center;
        rb2d.velocity = -distanceVec*1.5f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Thorn"))
        {
            AdController.Instance.PlayVideoAD();
            GameManagement.Instance.GameOver();
        }
    }
    public void ChangeCharacter(Character character)
    {
        selectedCharacter = character;
        GetComponent<SpriteRenderer>().sprite = selectedCharacter.sprite;
    }
}

