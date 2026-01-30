using UnityEngine;

public class BasketManager : MonoBehaviour
{
    public float basket_x;
    public float basket_speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            // Limit basket movement to screen bounds
            if (basket_x > -7.5f){
                basket_x -= basket_speed;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            // Limit basket movement to screen bounds
            if (basket_x < 7.5f){
                basket_x += basket_speed;
            }
        }
        transform.position = new Vector3(basket_x, -3.5f, 0);
    }
}
