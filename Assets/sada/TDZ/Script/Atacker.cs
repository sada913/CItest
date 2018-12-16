using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Atacker : MonoBehaviour
{

    private RaycastHit hit;
    private Ray ray;
    private PhotonView m_photonView;
    private Transform m_camera;
    private NavMeshHit navhit;

    public Vector3 HitGroundPosition;

    [SerializeField] GameObject HitPosition;

    void Start()
    {
        m_photonView = GetComponent<PhotonView>();
        m_camera = GameObject.FindWithTag("MainCamera").transform;
        if (m_photonView.isMine)
            HitPosition = Instantiate(HitPosition, transform.position, transform.rotation);
    }
    void Update()
    {
        if (m_photonView.isMine)
        {
            ray = new Ray(m_camera.position, m_camera.forward);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (NavMesh.SamplePosition(hit.point, out navhit, 0.1f, NavMesh.AllAreas))
                    HitGroundPosition = navhit.position;
                HitPosition.transform.position = HitGroundPosition;
            }
        }
    }
}
