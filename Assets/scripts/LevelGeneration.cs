using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
    public GameObject tp_room;
    public float moveammount;
    public float startTimeBtwRoom = 0.25f;
    public float minX;
    public float maxX;
    public float minY;
    public LayerMask room;
    public bool stop = true;
    public GameObject player;
    private int direction;
    private float timeBtwRoom = 0f;
    private Vector2 newPos = Vector2.zero;
    private int preced = 0;
    private int down_count = 0;

    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        GameObject crroom = Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
        preced = direction;
        Debug.Log("starting point = " + transform.position);
        GameObject playeri = Instantiate(player, transform.position, Quaternion.identity);
        playeri.transform.position = crroom.GetComponent<RoomType>().startPlayerPos.transform.position;
        playeri.GetComponent<characterMovement>().levelGeneration = gameObject.GetComponent<LevelGeneration>();
    }

    int get_random_bottom() {
        int rand = Random.Range(0, 3);
        Debug.Log("Room: " + (rand == 1 ? "3" : "1"));
        return (rand == 1 ? 3 : 1);
    }

    void Move() {
        int rand = 0;
        Collider2D cild;
        if (direction == 1 || direction == 2) {
            if (transform.position.x < maxX) {
                down_count = 0;
                newPos = new Vector2(transform.position.x + moveammount, transform.position.y);
                transform.position = newPos;
                direction = Random.Range(1, 6);
                rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                if (direction == 3) {
                    direction = 2;
                } else if (direction == 4) {
                    direction = 5;
                }
            } else {
                direction = 5;
                return;
            }
        } else if (direction == 3 || direction == 4) {
            down_count = 0;
            if (transform.position.x > minX) {
                newPos = new Vector2(transform.position.x - moveammount, transform.position.y);
                transform.position = newPos;
                direction = Random.Range(3, 6);
                rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
            } else {
                direction = 5;
                return;
            }
        } else if (direction == 5) {
            if (transform.position.y > minY) {
                down_count++;
                cild = Physics2D.OverlapCircle(transform.position, 1, room);
                if (cild.GetComponent<RoomType>().type == 0 || cild.GetComponent<RoomType>().type == 2) {
                    cild.GetComponent<RoomType>().roomDestroy();
                    Debug.Log("down_count = " + down_count);
                    if (down_count < 2) {
                        rand = get_random_bottom();
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    } else {
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                }
                newPos = new Vector2(transform.position.x, transform.position.y - moveammount);
                transform.position = newPos;
                direction = Random.Range(1, 6);
                rand = Random.Range(2, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
            } else {
                cild = Physics2D.OverlapCircle(transform.position, 1, room);
                cild.GetComponent<RoomType>().roomDestroy();
                Instantiate(tp_room, transform.position, Quaternion.identity);
                stop = false;
                return;
            }
        }
        preced = direction;
    }

    void Update()
    {
        if (timeBtwRoom <= 0 && stop) {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else if (stop) {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
