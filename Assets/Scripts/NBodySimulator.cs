using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NBodySimulator : MonoBehaviour {

    public static float gravitationalConstant = 6.673E-11f; //  m^3 kg^-1 s^-1 

    public List<NBodyObject> nBodyObjects;
    
    public float G = 6.673E-11f; //  m^3 kg^-1 s^-1 
    public float lastDt = 0f;
    public float dtScale = 100f;
    public float dtScaleMultiplier = 10f;

    // Use this for initialization
    void Start () {
        
        nBodyObjects = GameObject.FindObjectsOfType<NBodyObject>().ToList();

	}
	
	// Update is called once per frame
	void Update () {
        lastDt = Time.deltaTime;

        for(var ii = 0; ii < nBodyObjects.Count; ii++)
        {
            for(var jj = 0; jj < nBodyObjects.Count; jj++)
            {
                if (ii == jj)
                    continue;
                nBodyObjects[ii].AddBodyForce(nBodyObjects[jj], G);
                checkCollisions(nBodyObjects[ii], nBodyObjects[jj]);
            }
        }

        foreach ( var nBodyObject in nBodyObjects )
        {
            nBodyObject.DoUpdate(Time.deltaTime * dtScale);
        }

        var unmergedObjects = new List<NBodyObject>();
        foreach ( var nBodyObject in nBodyObjects )
        {
            if (nBodyObject.hasBeenMerged)
                continue;
            unmergedObjects.Add(nBodyObject);
            if (nBodyObject.toBeMerged)
            {
                mergeNBodies(nBodyObject);
            }
        }
        
        var mergedObjects = nBodyObjects.Except(unmergedObjects);
        foreach (var mo in mergedObjects)
        {
            Debug.Log("Deleting "+mo.gameObject.name);
            GameObject.Destroy(mo.gameObject);
        }
        nBodyObjects = unmergedObjects;
	}

    private void mergeNBodies(NBodyObject mergeObject)
    {
        var mergeList = mergeObject.mergeList;
        mergeList.Add(mergeObject); //hacks omg
        var count = mergeList.Count;
        var averageForce = Vector3.zero;
        var averagePosition = Vector3.zero;
        var averageVelocity = Vector3.zero;
        float totalMass = 0;

        foreach(var nBodyObject in mergeList)
        {
            averageForce += nBodyObject.mass * nBodyObject.force;
            averagePosition += nBodyObject.mass * nBodyObject.transform.position;
            averageVelocity += nBodyObject.mass * nBodyObject.velocity;
            totalMass += nBodyObject.mass;
            nBodyObject.hasBeenMerged = true;
        }
        averageForce /= count;
        averagePosition /= count;
        averageVelocity /= count;

        mergeObject.mass = totalMass;
        mergeObject.force = averageForce / totalMass;
        mergeObject.velocity = averageVelocity / totalMass;
        //mergeObject.transform.position = averagePosition / totalMass;
        mergeObject.hasBeenMerged = false;
        mergeObject.toBeMerged = false;
        mergeObject.mergeList = new List<NBodyObject>();
    }

    private void checkCollisions(NBodyObject a, NBodyObject b)
    {

        //Merges
        var mergeDistance = (a.radius + b.radius)/2;
        var distance = Vector3.Distance(a.transform.position, b.transform.position);
        if ( distance < mergeDistance)
        {
            Debug.LogWarning("Bloop!");
            a.toBeMerged = true;
            a.mergeList.Add(b);
            b.toBeMerged = true;
        }
    }
}
