using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHealth : MonoBehaviour
{
    [SerializeField] public ManageBoss boss;
    public int maxHealth = 1500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(((float)boss.health/(float)maxHealth));
        transform.localScale = new Vector3(((float)boss.health/(float)maxHealth) * 3f, 0.2f, 0);
        if (boss.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
