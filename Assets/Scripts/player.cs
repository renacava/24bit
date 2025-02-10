using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public float movementSpeed = 10;
    float speedX, speedY;
    public float health = 100;
    Rigidbody2D playerRigidBody;
    List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    List<GameObject> touchedGameObjects = new List<GameObject>();
    bool isTouchingEnemy = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseRigidBody();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessDamage();
        MovePlayer();
    }

    void InitialiseRigidBody(){
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.gravityScale = 0;
    }

    void MovePlayer(){
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        playerRigidBody.linearVelocity = new Vector2(speedX, speedY);
        Director.UpdatePlayerPosition((Vector2)transform.position);
    }

    void ProcessDamage(){
        UpdateIsTouchingEnemy();
        if (isTouchingEnemy)
            TakeDamage();
        if (health <= 0)
            Director.RestartScene();
    }

    void TakeDamage(){
        health -= 0.5f;
    }

    void UpdateIsTouchingEnemy(){
        UpdateTouchedThings();
        isTouchingEnemy = false;
        foreach (GameObject touchedGameObject in touchedGameObjects){
            if (touchedGameObject.tag == "Enemy"){
                isTouchingEnemy = true;
                break;
            }
        }
    }

    void UpdateTouchedThings(){
        playerRigidBody.GetContacts(contacts);
        touchedGameObjects.Clear();
        foreach (ContactPoint2D contact in contacts){
            touchedGameObjects.Add(contact.collider.gameObject);
        }
    }


}
