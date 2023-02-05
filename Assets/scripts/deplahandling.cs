using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplahandling : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject camera;
    public bool is_second = false;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
        if (is_second)
            gameObject.transform.position = new Vector3(camera.transform.position.x + gameObject.GetComponent<Renderer>().bounds.size.x, transform.position.y, transform.position.z);
        else
            gameObject.transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (gameObject.transform.position.x <= camera.transform.position.x - gameObject.GetComponent<Renderer>().bounds.size.x) {
            transform.position = new Vector3(camera.transform.position.x + gameObject.GetComponent<Renderer>().bounds.size.x, transform.position.y, transform.position.z);
        } else if (gameObject.transform.position.x >= camera.transform.position.x + gameObject.GetComponent<Renderer>().bounds.size.x) {
            transform.position = new Vector3(camera.transform.position.x - gameObject.GetComponent<Renderer>().bounds.size.x, transform.position.y, transform.position.z);
        } else {
            if (Input.GetKey(KeyCode.LeftArrow))
                gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.RightArrow))
                gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
