using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    [Tooltip("爆発するまでの時間")]
    [SerializeField] float StartTime = 3f;
    [Tooltip("爆発後消えるまでの時間")]
    [SerializeField] float EndTime = 1f;
    [SerializeField] GameObject BombColider;
	// Use this for initialization
	void Start () {
        StartCoroutine(Timer(StartTime));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(BombColider, transform.position, transform.rotation);
        Destroy(gameObject);


    }


}
