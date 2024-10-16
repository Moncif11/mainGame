using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject SP2ShotPrefab;
    public GameObject bulletPrefab;
    public Transform shootPoint; 
     public Boss1BT boss1BT; 

    void Start(){
        Boss1BT boss1BT = GetComponent<Boss1BT>();
        if (boss1BT == null) {
            Debug.LogError("Boss1BT component not found on the object " + gameObject.name);
        }
    }
    public void Shoot()
    {   
        Debug.Log("Boss ?" +boss1BT);
        bool isLeft = boss1BT.LeftLooking();
        FindObjectOfType<AudioManager>().Play("Shoot"); 
        if(!isLeft){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPoint.position + Vector3.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bullet.GetComponent<FireBullet>().damage =Boss1BT.attack;  
                bulletRB.AddForce(Vector2.right*150);
            }
            else{
                Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPoint.position + Vector3.left, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();   
                bullet.GetComponent<FireBullet>().damage =Boss1BT.attack;  
                bulletRB.AddForce(-Vector2.right*150);
                Debug.Log("Bullet : " + bullet.transform.position);
            }   
    }

     public void shootSP2()
    {   
        FindObjectOfType<AudioManager>().Play("Shoot");
        Debug.Log("Boss ?" +boss1BT);
        bool isLeft = boss1BT.LeftLooking();
        if(!isLeft){
                GameObject bullet = GameObject.Instantiate(SP2ShotPrefab, shootPoint.position + Vector3.right +Vector3.down, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();  
                bullet.GetComponent<Burn>().damage = (int)Boss1BT.attack;
                bulletRB.AddForce(Vector2.right*1000);
            }
            else{
                Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(SP2ShotPrefab, shootPoint.position + Vector3.left, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bullet.GetComponent<Burn>().damage =(int)Boss1BT.attack;  
                bulletRB.AddForce(-Vector2.right*1000);
                Debug.Log("Bullet : " + bullet.transform.position);
            }   
          Boss1BT.SP2Ready = false;  
    }
}


    