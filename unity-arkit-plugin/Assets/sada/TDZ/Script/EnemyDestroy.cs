using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour {
    [SerializeField] GameObject debugObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    
    private void OnTriggerEnter(Collider other)
    {
        //Instantiate(debugObj, transform.position, transform.rotation);
        Debug.Log(other.tag);

        if(other.CompareTag("Bomb"))
        {
            Destroy(gameObject);
            Debug.Log("爆発");

        }
    }
}
