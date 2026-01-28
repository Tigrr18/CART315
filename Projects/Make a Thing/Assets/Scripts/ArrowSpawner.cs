using UnityEngine;
using System.Collections;
using TMPro;

public class ArrowAppear : MonoBehaviour {
    // The prefab to spawn
    public GameObject arrowPrefab;
    public Transform[] positions;
    public Color32[] colors;
    public KeyCode[] keys;
    public float[] rotation;
    public float[] perfectPosition;
    public float number;
    [SerializeField] public TextMeshPro scoreText;
    [SerializeField] public TextMeshPro failText;
    public static bool isGameOver = false;
    private int spawnedArrows;
    private float speed;

    public static ArrowAppear Instance { get; private set; }


    private static int score;
    private static int fails;

    // Time between spawns
    private float[] timer = {1f, 1f, 2f, 2f, 2f, 2f, 3f, 3f, 3f, 4f, 4f};

    void Start() {
        Instance = this;
        ResetGame();
        scoreText.text = "Score: " + score;
        failText.text = "Fails: " + fails;
        StartCoroutine(randomTimer());
    }

    void Update() {
       
    }

    public void Scoring(int successLevel){
        if (isGameOver) return;
        switch (successLevel){
            case 0:
                score -= 5;
                fails++;
                scoreText.text = "Score: " + score;
                failText.text = "Fails: " + fails;
                if (fails >= 10){
                    // Game Over Logic Here
                    failText.text = "Game Over!";
                    isGameOver = true;
                }
                break;
            case 1: 
                score += 2;
                scoreText.text = "Score: " + score;
                break;
            case 2:
                score += 5;
                scoreText.text = "Score: " + score;
                break;
        }
    }

    void Spawn() {
        if (!isGameOver){
            spawnedArrows++;
            int index = Random.Range(0,4);

            // Instantiate the prefab
            GameObject arrowObj = Instantiate(arrowPrefab, positions[index]);

            arrowObj.GetComponent<ArrowFalling>().Init(colors[index], rotation[index], keys[index], perfectPosition[index], speed);
        }
    }

    IEnumerator randomTimer() {
        while (true) {
            int index = Random.Range(0,10);
            yield return new WaitForSeconds(timer[index]*number);
            Spawn();
            if (spawnedArrows >= 15){
                if (number > 0.1f){
                    number -= 0.1f;
                }
                speed *= 1.2f;
                spawnedArrows = 0;
            }
        }
    }

    public void ResetGame() {
        score = 0;
        fails = 0;
        speed = -4f;
        number = 0.5f;
        spawnedArrows = 0;
        scoreText.text = "Score: " + score;
        failText.text = "Fails: " + fails;
        isGameOver = false;
    }
}
