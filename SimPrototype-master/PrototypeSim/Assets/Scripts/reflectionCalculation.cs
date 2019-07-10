using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectionCalculation : MonoBehaviour
{

    public int distanceMax;

    Vector3 target;

    float perpDistance;
    RaycastHit hit;
    RaycastHit hitWall;
    RaycastHit hitReflect;
    RaycastHit reflect;
    RaycastHit hitLOS;

    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Collider wall in wallsNearby())
        {
            if (wall.gameObject.tag != "baseStation" && wall.gameObject.tag != "badReflect")
            { 
                calculate(wall);
                foreach (GameObject reciever in GameObject.FindGameObjectsWithTag("baseStation"))
                {
                    reflectCalculate(reciever);
                }
                
            }
        }
    }

    void calculate(Collider wall)
    {
        if (Physics.Raycast(transform.position, wall.transform.position - transform.position, out hitWall, distanceMax))
        {
            Vector3 perpAngle = hitWall.normal;
            float hitDistance = hitWall.distance;
            Vector3 hitDirection = wall.transform.position - transform.position;
            float hitAngle = Vector3.Angle((hitWall.normal), -hitDirection.normalized);

            perpDistance = hitDistance * Mathf.Cos((hitAngle * Mathf.PI) / 180);

            target = transform.position - (hitWall.normal * perpDistance * 2);

            
        }
    }

    void reflectCalculate(GameObject reciever)
    { 
        if (Physics.Raycast(reciever.transform.position, target - reciever.transform.position, out hit, distanceMax))
        {
            if (Physics.Raycast(transform.position, hit.point - transform.position, out hitReflect, distanceMax) && hit.point == hitReflect.point)
            {
                Debug.DrawLine(transform.position, transform.position - (hitWall.normal * perpDistance * 2), Color.red);
                Debug.DrawLine(reciever.transform.position, target, Color.red);
                Debug.DrawLine(transform.position, hit.point, Color.green);
                Debug.DrawLine(hit.point, reciever.transform.position, Color.green);
            }
        }       
    }


    private Collider[] wallsNearby()
    {
        Collider[] walls = Physics.OverlapSphere(transform.position, distanceMax);
        return walls;
    }
}
