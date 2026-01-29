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
            // Moves the object downwards based on speed and time
            transform.position += new Vector3 (0, _speed*Time.deltaTime, 0);
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

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Basket")){
            Destroy(gameObject);
            Spawner.Instance.ScorePoints();
        }
    }

    public void Init(Sprite image){
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
