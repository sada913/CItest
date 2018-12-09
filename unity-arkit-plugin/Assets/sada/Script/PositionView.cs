using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionView : MonoBehaviour {
    [SerializeField] Transform arcam;
    [SerializeField] Text text;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        text.text = "(" + arcam.transform.position.x + "," + arcam.transform.position.y + "," + arcam.transform.position.z + ")";
    }
}
