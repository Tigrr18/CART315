using UnityEngine;
using System.Collections;

public class ObjectFalling : MonoBehaviour
{
    [SerializeField] private float _speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the game is active
        if (Spawner.gameActive){
            // Destroys the object and ends the game if the object goes below a certain point
            if (transform.position.y < -4.12f){
                Spawner.Instance.EndGame();
                Destroy(gameObject);
            }
        } else {
            // Destroys game object if the game is not active
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Basket")) {
            Debug.Log("Caught by basket!");
            Spawner.Instance.ScorePoints();
            Destroy(gameObject);
        }   
    }

    public void Init(Sprite image){
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
