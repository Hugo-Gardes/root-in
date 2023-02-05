using UnityEngine;

public class spawnothers : MonoBehaviour {
    public LayerMask room;
    public LevelGeneration levelGeneration;

    void Update() {
        if (levelGeneration.stop)
            return;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1, room);
        int rand = 0;
        if (!collider) {
            rand = Random.Range(0, levelGeneration.rooms.Length);
            Instantiate(levelGeneration.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}