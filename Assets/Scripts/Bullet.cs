using UnityEngine;

public class Bullet : MonoBehaviour
{

    Vector2 direction = new Vector2(0, 0);
    float bulletSpeed = 12f;
    float damage = 1f;
    Rigidbody2D rigidBody;

    void Start()
    {
        InitialiseRigidBody();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move(){
        rigidBody.linearVelocity = direction.normalized * bulletSpeed;
        //rigidBody.linearVelocity = new Vector2(1, 0) * bulletSpeed;
    }

    void InitialiseRigidBody(){
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    public void SetDirection(Vector2 newDirection){
        direction = newDirection;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        ResolveCollision(collision2D.gameObject);
    }

    void ResolveCollision(GameObject hitObject){
        switch (hitObject.tag){
            case "Enemy":
                OnHitEnemy(hitObject);
                break;
            case "Player":
                break;
            case "Bullet":
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    void OnHitEnemy(GameObject hitObject){
        Enemy enemy = hitObject.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
