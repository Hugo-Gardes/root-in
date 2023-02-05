using UnityEngine;

public class RoomType : MonoBehaviour {
    public int type;
    public GameObject startPlayerPos;

    public void roomDestroy() {
        Debug.Log("Destroying room");
        Destroy(gameObject);
    }
}