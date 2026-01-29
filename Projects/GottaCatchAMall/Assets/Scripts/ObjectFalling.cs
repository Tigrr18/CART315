using UnityEngine;
using System.Collections;

public class ObjectFalling : MonoBehaviour
{
    [SerializeField] private float _speed;
    private GameObject _basket;

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

    public void Init(Sprite image, GameObject basket){
        GetComponent<SpriteRenderer>().sprite = image;
        _basket = basket;
    }
}
