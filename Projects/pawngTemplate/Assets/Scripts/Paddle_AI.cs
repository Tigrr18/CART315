using UnityEngine;
using System.Collections;

public class Paddle_AI : MonoBehaviour
{

    private float     yPos;
    public float      paddleSpeed = .03f;
    // private int randomTimer = 0;

    private float ballYPos = 0f;
    private float targetYPos = 0f;

	public float	  topWall, bottomWall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        RamdomizeTargetPosition();
    }

    // Update is called once per frame
    void Update() {
        /*
        if (randomTimer <= 0) {
            randomTimer = Random.Range(100,200);
            RamdomizeTargetPosition();
        } else {
            randomTimer--;
            MovePaddle();
        } 
        */
        MovePaddle();
        transform.localPosition = new Vector3(transform.position.x, yPos, 0);
    }

    float GetBallYPosition() {
        GameObject ball = GameObject.FindWithTag("Ball");
        return ball.transform.position.y;
    }

    /*
    float GetBallXPosition() {
        GameObject ball = GameObject.FindWithTag("Ball");
        return ball.transform.position.x;
    }
    */

    void RamdomizeTargetPosition() {
        ballYPos = GetBallYPosition();
        targetYPos = ballYPos + Random.Range(-1.5f, 1.5f);
        targetYPos = Mathf.Round(targetYPos * 10f) / 10f; // Round to nearest 0.1
    }

    void MovePaddle() {
        Debug.Log("Current Y Pos: " + yPos);
        Debug.Log("Target Y Pos: " + targetYPos);
        if (targetYPos > yPos-0.01f && targetYPos < yPos+0.01f) {
            RamdomizeTargetPosition();
        }
        
        if (targetYPos > yPos && yPos < topWall) {
            yPos += paddleSpeed;
        } else if (targetYPos < yPos && yPos > bottomWall) {
            yPos -= paddleSpeed;
        }
        Debug.Log("New Y Pos: " + yPos);
        Debug.Log("-----");
    }
}
