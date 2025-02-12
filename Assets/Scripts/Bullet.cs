using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 4f;
    Rigidbody2D rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseRigidBody();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.linearVelocity = new Vector2(bulletSpeed, 0);
    }

    void InitialiseRigidBody(){
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision2D){
        if (collision2D.gameObject.tag == "Enemy"){
            Debug.Log($"Hit enemy {collision2D.gameObject}");
            Destroy(gameObject);
        }
    }
}
