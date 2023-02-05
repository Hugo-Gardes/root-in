using UnityEngine;

public class ex_clock : MonoBehaviour {
    public float time = 60f;
    public float starting = 60f;

    private void Update() {
        if (time <= 0) {
            Debug.Log("Time's up!");
            time = starting;
            return;
        }
        time -= Time.deltaTime;
    }
}