using UnityEngine;

public class Bullet : MonoBehaviour
{

    Vector2 direction = new Vector2(0, 0);
    public float bulletSpeed = 8f;
    public float damage = 10f;
    Rigidbody2D rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseRigidBody();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.linearVelocity = direction.normalized * bulletSpeed;
    }

    public void SetDirection(Vector2 newDirection){
        direction = newDirection;
    }

    void InitialiseRigidBody(){
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision2D){
        GameObject hitObject = collision2D.gameObject;
        if (hitObject.tag == "Enemy"){
            Enemy enemy = hitObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
