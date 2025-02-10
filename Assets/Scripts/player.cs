using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public float movementSpeed = 10;
    float speedX, speedY;
    public float health = 100;
    Rigidbody2D playerRigidBody;
    List<ContactPoint2D> contacts = new List<ContactPoint2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TakeDamage();
        ProcessDamage();
        MovePlayer();
        Director.UpdatePlayerPosition((Vector2)transform.position);
    }

    void MovePlayer(){
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        playerRigidBody.linearVelocity = new Vector2(speedX, speedY);
    }



    void ProcessDamage(){
        
        playerRigidBody.GetContacts(contacts);
        if (contacts.Count > 0)
            TakeDamage();
        if (health <= 0)
            Director.RestartScene();
    }

    void TakeDamage(){
        health -= 0.5f;
    }



}
