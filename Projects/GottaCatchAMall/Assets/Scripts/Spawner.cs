using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    private float maxX = -7.62f;
    private float minX = 7.62f;
    private float spawnY = 6.21f;
    private Vector3 spawnPosition;
    private float timer;
    public static bool gameActive = true;

    public static Spawner Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Instance = this;
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update() {
        
    }

    IEnumerator SpawnTimer() {
        while (gameActive) {
            timer = Random.Range(0.25f, 2f);
            yield return new WaitForSeconds(timer);
            SpawnObject();
        }
    }

    public void SpawnObject() {
        // Select a random spawnable object
        int randomIndex = Random.Range(0, images.Length);
        // Generate a random X position within the specified range
        float randomX = Random.Range(maxX, minX);
        // Set the spawn position
        spawnPosition = new Vector3(randomX, spawnY, 0);
        // Instantiate the selected object at the spawn position
        Instantiate(images[randomIndex], spawnPosition, Quaternion.identity);
    }

    public void ResetGame() {
        gameActive = true;
    }
}
