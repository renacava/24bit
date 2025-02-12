using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    //movement
    public float movementSpeed = 10;
    float speedX, speedY;
    
    //collision
    Rigidbody2D playerRigidBody;
    List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    List<GameObject> touchedGameObjects = new List<GameObject>();
    bool isTouchingEnemy = false;

    //health
    public float maxHealth = 100;
    public float health = 1;
    public Image healthbarImage;

    //bullets
    public GameObject bulletPrefab;
    
    void Start()
    {
        InitialiseRigidBody();
        ResetHealth();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        ProcessDamage();
        MovePlayer();
        
        
    }

    void InitialiseRigidBody(){
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.gravityScale = 0;
    }

    void ResetHealth(){
        health = maxHealth;
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
        UpdateHealthbar();
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

    void UpdateHealthbar(){
        healthbarImage.fillAmount = health / maxHealth;
    }

}
