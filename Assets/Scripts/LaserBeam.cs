using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private LineRenderer line;
    private RaycastHit2D hit;
    [SerializeField] private float takeDamage = 5; 
    Transform m_transform; 
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>(); 
        m_transform = GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.Raycast(m_transform.position , -transform.right )){
            hit = Physics2D.Raycast(m_transform.position, -transform.right);    
            line.SetPosition(0,m_transform.position);
            line.SetPosition(1,hit.point);
            if(hit.collider.CompareTag("Player")){
                Health playerHealth = hit.collider.GetComponent<Health>();
                if(playerHealth != null){
                    playerHealth.takeDamage(takeDamage);
                }
            }
        }   
    }
}
