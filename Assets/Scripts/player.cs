using UnityEngine;

public class Player : MonoBehaviour
{

    public float movementSpeed = 10;
    float speedX, speedY;
    Rigidbody2D playerRigidBody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
        Director.UpdatePlayerPosition((Vector2)transform.position);
    }

    void movePlayer(){
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        playerRigidBody.linearVelocity = new Vector2(speedX, speedY);
    }

}
