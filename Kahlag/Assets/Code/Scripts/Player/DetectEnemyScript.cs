﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyScript : MonoBehaviour
{

    public Transform EnemyToTarget;


    public float DetectionRadius;

    public Transform Head;

    public GameObject[] enemies;

    //public float distance = 3f;
    //float nearestDistance = 50f;
    public float VisionRadius = 90f;



    void Start()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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

    public void AddEnemyToArray()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void LookForEnemy()
    {

        float distance = 3f;
        float nearestDistance = 50f;

        var colliders = Physics.OverlapSphere(transform.position, DetectionRadius);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                distance = (transform.position - collider.transform.position).sqrMagnitude;
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    EnemyToTarget = collider.transform;
                }
            }
        }

        //foreach (GameObject enemy in enemies)
        //{

        //    Vector3 targetDir = enemy.transform.position - transform.position;
        //    float angle = Vector3.Angle(targetDir, transform.forward);

        //    if (Vector3.Distance(enemy.transform.position, transform.position) < distance && (angle < VisionRadius))
        //    {
        //        print("Enemy in front");
        //        EnemyToTarget = enemy.transform;
        //    }


        //    else if (Vector3.Distance(transform.position, enemy.transform.position) < distance)
        //    {
        //        print("Enemy near");
        //        EnemyToTarget = enemy.transform;
        //    }

        //    else
        //    {
        //        print("No Target");
        //        EnemyToTarget = null;
        //    }
        //}

        //Head.rotation = Quaternion.LookRotation(hit.normal);
        //EnemyToTarget = hit.transform;
        //Head.LookAt(EnemyToTarget);
    }
    //}
}
