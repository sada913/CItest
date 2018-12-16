using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spooner : MonoBehaviour {
    [SerializeField] float Intervaltime = 2f;
    [SerializeField] float Range = 1f;
    // ランダム生成の範囲
    [SerializeField] int MaxEnemy = 5;



    [SerializeField] GameObject Enemy;

    PhotonView m_photonView;

    // Use this for initialization

	void Start () {
        GameMaster.Instance.MaxEnemy = MaxEnemy;
        StartCoroutine(SpooneEnemy());
        m_photonView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpooneEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Intervaltime);
            Vector3 point;
            if (RandomPoint(Vector3.zero, Range, out point) && GameMaster.Instance.Player != null && GameMaster.Instance.EnemyCounts < MaxEnemy && PhotonNetwork.isMasterClient && PhotonNetwork.room.PlayerCount == 2)
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                PhotonNetwork.Instantiate(Enemy.name, point, transform.rotation,0);
                GameMaster.Instance.EnemyCounts++;
            }

        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
