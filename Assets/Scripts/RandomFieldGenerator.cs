using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFieldGenerator : MonoBehaviour {

    public GameObject bodyPrefab;
    public int numBodies = 10;
    public float maxMass = 2000000;
    public float minMass = 10000;

    public GameObject ss_tl;
    public GameObject ss_br;
    
    public List<NBodyObject> GenerateField()
    {
        var generated = new List<NBodyObject>();
        for(var ii = 0; ii < numBodies; ii++)
        {
            generated.Add(GenerateBody());
        }
        return generated;
    }

    public NBodyObject GenerateBody()
    {
        var body = GameObject.Instantiate(bodyPrefab, gameObject.transform);
        var nBody = body.GetComponent<NBodyObject>();
        var xPos = Vector3.Lerp(ss_tl.transform.position, ss_br.transform.position, Random.value).x;
        var yPos = Vector3.Lerp(ss_tl.transform.position, ss_br.transform.position, Random.value).y;
        var position = new Vector3(xPos, yPos);
        body.transform.position = position;
        nBody.mass = Mathf.Lerp(maxMass, minMass, Random.value);
        return nBody;
    }

}
