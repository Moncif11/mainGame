using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Minion : MonoBehaviour
{   
    public GameObject PointA; 
    public GameObject PointB; 

    private  Transform currentPoint; 
    private Rigidbody2D rb;    
    [Header("Stats")]
    public float damage = 2;
    public float speed = 3;
    // Start is called before the first frame update
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint== PointB.transform){
            rb.velocity = new Vector2(speed,0);
        }
        else{
            rb.velocity = new Vector2(-speed,0);
        }

        if(Vector2.Distance(transform.position,currentPoint.position)<0.5f && currentPoint == PointB.transform){
            Flip(); 
            currentPoint = PointA.transform;
        }
          if(Vector2.Distance(transform.position,currentPoint.position)<0.5f && currentPoint == PointA.transform){
            Flip(); 
            currentPoint = PointB.transform;
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f); 
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f); 
        Gizmos.DrawLine(PointA.transform.position,PointB.transform.position); 

    }

    private void Flip(){
        Vector3 Scaler = transform.localScale;
        Scaler.x*= -1; 
        transform.localScale = Scaler;
    }

    void OnCollisionEnter2D(Collision2D other){
            if(other.gameObject.CompareTag("Player")){}
                other.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            }
}
