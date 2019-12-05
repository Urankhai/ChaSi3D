using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoS_visualization : MonoBehaviour
{
    private LineRenderer lr1, lr2;
    public Transform[] ant_rx;
    public Transform ant_tx;

    void Start()
    {
        lr1 = GetComponent<LineRenderer>();
        lr2 = GetComponent<LineRenderer>();
    }

    void Update()
    {
        /*RaycastHit hit1;
        if (Physics.Raycast(transform.position, ant_rx[0].position, out hit1))
        {
            lr.SetPosition(1, ant_rx[0].position);
        }

        RaycastHit hit2;
        if (Physics.Raycast(transform.position, ant_rx[1].position, out hit2))
        {
            lr.SetPosition(1, ant_rx[1].position);
        } */

        /*foreach (Transform ant in ant_rx)
        {
            lr.SetPosition(0, new Vector3(ant_tx.transform.position.x, ant_tx.transform.position.y, ant_tx.transform.position.z));
            lr.SetPosition(1, new Vector3(ant.transform.position.x, ant.transform.position.y, ant.transform.position.z));
        } */

        //lr1.SetPosition(0, new Vector3(ant_tx.transform.position.x, ant_tx.transform.position.y, ant_tx.transform.position.z));
        //lr1.SetPosition(1, new Vector3(ant_rx[0].transform.position.x, ant_rx[0].transform.position.y, ant_rx[0].transform.position.z));

        //lr2.SetPosition(0, new Vector3(ant_tx.transform.position.x, ant_tx.transform.position.y, ant_tx.transform.position.z));
        //lr2.SetPosition(1, new Vector3(ant_rx[1].transform.position.x, ant_rx[1].transform.position.y, ant_rx[1].transform.position.z));
        
    }
}
