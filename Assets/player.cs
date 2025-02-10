using UnityEngine;

public class player : MonoBehaviour
{

    public float movementSpeed = 1500;
    float speedX, speedY;
    Rigidbody2D playerRigidBody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer(){
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        playerRigidBody.linearVelocity = new Vector2(speedX, speedY);
    }

}
