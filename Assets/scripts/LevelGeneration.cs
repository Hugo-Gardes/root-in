using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
    public float moveammount;
    public float startTimeBtwRoom = 0.25f;
    private int direction;
    private float timeBtwRoom = 0f;

    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(0, 6);
    }

    void Move() {
        Vector2 newPos = Vector2.zero;
        if (direction == 1 || direction == 2) {
            newPos = new Vector2(transform.position.x + moveammount, transform.position.y);
        } else if (direction == 3 || direction == 4) {
            newPos = new Vector2(transform.position.x - moveammount, transform.position.y);
        } else if (direction == 5) {
            newPos = new Vector2(transform.position.x, transform.position.y + moveammount);
        }
        transform.position = newPos;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(0, 6);
    }

    void Update()
    {
        if (timeBtwRoom <= 0) {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
