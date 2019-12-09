using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scatterers_Finder : MonoBehaviour
{
    public float Vision_Area;
    private GameObject[] dmcs;
    private GameObject[] mat_spheres;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Position of the antenna "+transform.position);
        dmcs = GameObject.FindGameObjectsWithTag("DMC");
        Debug.Log("Number of DMCs = " + dmcs.Length);

        mat_spheres = GameObject.FindGameObjectsWithTag("materials");
        //Debug.Log("Number of seen DMCs = "+visibleDMCs.Length);

    }

    // Update is called once per frame
    void Update()
    {
        //var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Name");
        //Debug.Log("Number of DMCs seen from the antenna = " + object);

        //List<GameObject> visibleDMCs;
        for (int i = 0; i < dmcs.Length - 1; i++)
        {
            Vector3 AB = transform.position - dmcs[i].transform.position;
            if (AB.magnitude < Vision_Area)
            {
                RaycastHit hit;
                if (Physics.Raycast(dmcs[i].transform.position, AB, out hit))
                {
                    if (hit.collider.tag == "Antenna")
                    {
                        var objectRenderer = dmcs[i].GetComponent<Renderer>();
                        objectRenderer.material.SetColor("_Color", Color.red);
                    }
                    else
                    {
                        var objectRenderer = dmcs[i].GetComponent<Renderer>();
                        var init_mat = mat_spheres[2].GetComponent<Renderer>();
                        objectRenderer.material = init_mat.material;
                        //objectRenderer.material.SetColor("_Color", Color.grey);

                    }

                }
            }
            else
            {
                var objectRenderer = dmcs[i].GetComponent<Renderer>();
                var init_mat = mat_spheres[2].GetComponent<Renderer>();
                objectRenderer.material = init_mat.material;
                //objectRenderer.material.SetColor("_Color", Color.grey);

            }
        }


    }
}
