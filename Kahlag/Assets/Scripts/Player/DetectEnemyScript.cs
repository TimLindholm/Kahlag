using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyScript : MonoBehaviour
{

    public Transform EnemyToTarget;


    public float DetectionRadius;

    public Transform Head;

    public GameObject[] enemies;

    public float distance = 3f;
    public float VisionRadius = 90f;



    void Start()
    {
  

    }

    void Update()
    {
         
        LookForEnemy();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);

    }


    public void LookForEnemy()
    {

        Debug.Log(EnemyToTarget, transform);
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, DetectionRadius, transform.forward, out hit) && hit.transform.tag == "Enemy")
        {
           

                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemy in enemies)
                {
                    Vector3 targetDir = enemy.transform.position - transform.position;
                    float angle = Vector3.Angle(targetDir, transform.forward);

                if (Vector3.Distance(enemy.transform.position, transform.position) < distance && (angle < VisionRadius))
                    {
                    EnemyToTarget = enemy.transform;
                    }


                if (Vector3.Distance(transform.position, enemy.transform.position) < distance)
                    {
                        EnemyToTarget = enemy.transform;
                    }

                    else
                    {
                        EnemyToTarget = null;
                    }
                }

            //Head.rotation = Quaternion.LookRotation(hit.normal);
            //EnemyToTarget = hit.transform;
            //Head.LookAt(EnemyToTarget);
        }
    }
}
