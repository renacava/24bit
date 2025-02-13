using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    //movement
    public float movementSpeed = 10;
    float speedX, speedY;
    Vector2 lastMovementDirection = new Vector2(1, 0);
    Vector2 movementDirection = new Vector2(0, 0);
    
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
    public GameObject bulletSpawner;
    public AudioSource gunAudio;
    public AudioClip shootSound;
    
    //enemies
    public List<GameObject> enemyPrefabs;
    public float spawnRange = 10f;

    void Start()
    {
        InitialiseRigidBody();
        ResetHealth();
        InvokeRepeating("SpawnEnemy", 1, 2);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
            FireBullet();
        if (Input.GetKeyDown(KeyCode.E))
            SpawnEnemy();
        
    }

    void FixedUpdate()
    {
        ProcessDamage();
        MovePlayer();
        UpdateBulletSpawnerTransform();
    }

    void InitialiseRigidBody(){
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.gravityScale = 0;
    }

    void ResetHealth(){
        health = maxHealth;
    }

    void FireBullet(){
        Vector2 spawnPosition = bulletSpawner.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, lastMovementDirection);
        Bullet newBullet = Instantiate(bulletPrefab, spawnPosition, toRotation).GetComponent<Bullet>();
        newBullet.SetDirection(lastMovementDirection);
        gunAudio.PlayOneShot(shootSound);
    }

    void MovePlayer(){
        UpdateMovementDirection();
        playerRigidBody.linearVelocity = movementDirection * movementSpeed;
        Director.UpdatePlayerPosition((Vector2)transform.position);
    }

    void UpdateMovementDirection(){
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        movementDirection = movementDirection.normalized;
        if (movementDirection != new Vector2(0, 0))
            lastMovementDirection = movementDirection;
    }

    void UpdateBulletSpawnerTransform(){
        bulletSpawner.transform.position = (Vector2)transform.position + lastMovementDirection;
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

    float Random1Minus1(){
        if (Random.Range(-1, 1) > 0)
            return 1;
        else
            return -1;
    }

    Vector3 GetRandomPointAroundSelf(float distance){
        //Vector3 point = Random.onUnitSphere * distance;
        //return transform.position + (new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized * distance);
        //return Vector3.Scale(point + transform.position, new Vector3(1, 1, 0));
        return (Vector2)transform.position + (Random.insideUnitCircle.normalized * distance);
    }

    void SpawnEnemy(){
        int index = Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[index], GetRandomPointAroundSelf(spawnRange), Quaternion.identity);
    }

}
