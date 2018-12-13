using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GodTouches;
public class CreateBomb : MonoBehaviour {
    Atacker m_Atacker;
    [SerializeField] GameObject Bomb;
    PhotonView m_PhotonView;
	// Use this for initialization
	void Start () {
        m_Atacker = GetComponent<Atacker>();
        m_PhotonView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
        var phase = GodTouch.GetPhase();

        if(phase == GodPhase.Began && Input.touchCount == 1)
        { 
            if(m_PhotonView.isMine)
            {
                PhotonNetwork.Instantiate(Bomb.name, m_Atacker.HitGroundPosition, transform.rotation,0);
            }
        }


    }
}
