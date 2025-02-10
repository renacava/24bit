using UnityEngine;

public class enemy : MonoBehaviour
{

    public float movementSpeed = 1.25f;
    Rigidbody2D enemyRigidBody;
    Vector2 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyRigidBody.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTarget();
        MoveTowardsTarget();
    }

    void UpdateTarget(){
        target = Director.GetPlayerPosition();
    }

    void MoveTowardsTarget(){
        Vector2 targetHeading = target - (Vector2)transform.position;
        float targetDistance = targetHeading.magnitude;
        Vector2 targetDirection = targetHeading / targetDistance;

        enemyRigidBody.linearVelocity = targetDirection * movementSpeed;

    }
}
