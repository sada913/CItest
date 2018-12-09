using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;
    private PhotonView m_photonView;
    private Transform m_camera;

    [SerializeField] GameObject HitPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_photonView = GetComponent<PhotonView>();
        m_camera = GameObject.FindWithTag("MainCamera").transform;
        if(m_photonView.isMine)
            HitPosition = Instantiate(HitPosition, transform.position, transform.rotation);
    }
    void Update()
    {
        if (m_photonView.isMine)
        {
            ray = new Ray(m_camera.position, m_camera.forward);
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawLine(ray.origin, ray.direction * 100f, Color.red);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                agent.SetDestination(hit.point);
                HitPosition.transform.position = hit.point;
            }
        }
    }
}