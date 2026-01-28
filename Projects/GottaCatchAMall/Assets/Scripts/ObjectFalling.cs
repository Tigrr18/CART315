using UnityEngine;
using System.Collections;

public class ObjectFalling : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float maxY = -6f; //need to double check max Y position for destroying object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawner.gameActive){
            transform.position += new Vector3 (0, _speed*Time.deltaTime, 0);
            if (transform.position.y < maxY){
                Destroy(gameObject);

                // Add fail logic here
            }
        } else {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Basket")){
            Destroy(gameObject);

            // Add scoring logic here 
        }
    }
}
