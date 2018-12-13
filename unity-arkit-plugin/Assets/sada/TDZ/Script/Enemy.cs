using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] GameObject debugObj;
    Vector3 PlayerPos;
    NavMeshAgent m_agent;
    PhotonView m_photonView;

	// Use this for initialization
	void Start () {
        m_agent = GetComponent<NavMeshAgent>();
        m_photonView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		if(PhotonNetwork.isMasterClient)
        {
            m_agent.SetDestination(GameMaster.Instance.Player.transform.position);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        //Instantiate(debugObj, transform.position, transform.rotation);
        Debug.Log(other.tag);

        if(other.CompareTag("Bomb"))
        {
            m_photonView.RPC("DestoroyObj", PhotonTargets.All);
            //爆弾に当たったら消える処理RPC
        }
        else if(other.CompareTag("Player"))
        {
            if(GameMaster.Instance.CanAtack && m_photonView.isMine)
            {
                m_photonView.RPC("EnemyAtack", PhotonTargets.All);
            }
        }


    }



    [PunRPC]
    private void DestoroyObj()
    {
        GameMaster.Instance.EnemyCounts--;
        GameMaster.Instance.AddScore();
        Destroy(gameObject);
        Debug.Log("爆発");
    }

    [PunRPC]
    private void EnemyAtack()
    {
        GameMaster.Instance.Damage();
        GameMaster.Instance.NonAtackCoroutine();
    }

}
