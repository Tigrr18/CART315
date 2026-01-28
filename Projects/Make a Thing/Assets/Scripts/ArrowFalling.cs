using UnityEngine;
using System.Collections;

public class ArrowFalling : MonoBehaviour {
    [SerializeField] private GameObject triangle;
    [SerializeField] private GameObject rectangle;
    private float speed_;
    private bool isDestroyed = false;

    private float perfectPosition_;
    private KeyCode key_;
    private byte[] alphaTransform = {0, 25, 55};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (!ArrowAppear.isGameOver){
            transform.position += new Vector3 (0, speed_*Time.deltaTime, 0);
            if (!isDestroyed) {
                if (transform.position.y <= (perfectPosition_ - 0.5f)){
                    StartCoroutine(Shine(0));
                    ArrowAppear.Instance.Scoring(0);
                }
                if (Input.GetKeyDown(key_)){
                    if(transform.position.y > (perfectPosition_ - 0.25f) && transform.position.y < (perfectPosition_ + 0.25f)){
                        StartCoroutine(Shine(2));
                        ArrowAppear.Instance.Scoring(2);
                    } else if(transform.position.y > (perfectPosition_ - 0.5f) && transform.position.y < (perfectPosition_ + 0.5f)){
                    StartCoroutine(Shine(1)); 
                        ArrowAppear.Instance.Scoring(1);
                    }
                }
            }
        } else {
            Destroy(gameObject);
        }
    }

    public void Init (Color32 color, float rotation, KeyCode key, float perfectPosition, float speed){
        triangle.GetComponent<SpriteRenderer>().color = color;
        rectangle.GetComponent<SpriteRenderer>().color = color;
        key_ = key;
        perfectPosition_ = perfectPosition;
        speed_ = speed;
        transform.eulerAngles = new Vector3(0,0,rotation);
    }

    IEnumerator Shine(int successLevel) {
        isDestroyed = true;
        if (successLevel > 0){
            triangle.GetComponent<SpriteRenderer>().color += new Color32(alphaTransform[successLevel], alphaTransform[successLevel], alphaTransform[successLevel], alphaTransform[successLevel]);
            rectangle.GetComponent<SpriteRenderer>().color += new Color32(alphaTransform[successLevel], alphaTransform[successLevel], alphaTransform[successLevel], alphaTransform[successLevel]);
            if (successLevel == 2){
                transform.localScale *= 1.2f;
            }
            speed_ *= 2f;
            yield return new WaitForSeconds(0.3f);
        } else {
            triangle.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            rectangle.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            speed_ *= 0.5f;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }


}
