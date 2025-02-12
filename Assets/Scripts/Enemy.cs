using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float movementSpeed = 1.25f;
    public float maxHealth = 2f;
    public float health = 2f;
    Rigidbody2D enemyRigidBody;
    Vector2 target;

    void Start()
    {
        SetupRigidbody();
        ResetHealth();
    }

    void FixedUpdate()
    {
        UpdateTarget();
        MoveTowardsTarget();
    }

    void SetupRigidbody(){
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyRigidBody.gravityScale = 0;
    }

    void UpdateTarget(){
        target = Director.GetPlayerPosition();
    }

    void MoveTowardsTarget(){
        enemyRigidBody.linearVelocity = CalculateTargetDirection() * movementSpeed;
    }

    Vector2 CalculateTargetDirection(){
        Vector2 targetHeading = target - (Vector2)transform.position;
        float targetDistance = targetHeading.magnitude;
        return targetHeading / targetDistance;
    }

    void ResetHealth(){
        health = maxHealth;
    }

    public void TakeDamage(float amount){
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die(){
        Destroy(gameObject);
    }
}
