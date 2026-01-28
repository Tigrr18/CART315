using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    // Variables
    [SerializeField] private Sprite[] images;
    [SerializeField] private GameObject spawnableObjectPrefab;
    private float maxX = -7.62f;
    private float minX = 7.62f;
    private float spawnY = 6.21f;
    private Vector3 spawnPosition;
    private float timer;

    // Static variables
    public static bool gameActive = true;
    public static Spawner Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // technically, I should be doing a proper singleton and checking for existing 
        // instance, but i know this script will only be assigned to a single object in the scene
        Instance = this; 
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Coroutine to handle spawning at random intervals
    IEnumerator SpawnTimer() {
        // Loop to spawn objects while the game is active
        while (gameActive) {
            // Generate a random time interval between spawns
            timer = Random.Range(0.25f, 2f);
            // Wait for the generated time interval
            yield return new WaitForSeconds(timer);
            // Spawn the object
            SpawnObject();
        }
    }

    // Method to handle the spawning of the objects
    public void SpawnObject() {
        // Select a random spawnable object
        int randomIndex = Random.Range(0, images.Length);
        // Generate a random X position within the specified range
        float randomX = Random.Range(maxX, minX);
        // Set the spawn position
        spawnPosition = new Vector3(randomX, spawnY, 0);
        // Instantiate the prefab object at the spawn position
        GameObject spawnedObject = GameInstantiate(spawnableObjectPrefab, spawnPosition, Quaternion.identity);
        // Assign the selected image to the spawned object
        spawnedObject.GetComponent<ObjectFalling>().Init(images[randomIndex]);
    }

    public void ResetGame() {
        gameActive = true;
    }
}
