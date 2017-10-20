using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour {

    public NBodySimulator nBodySimulator;
    public RandomFieldGenerator rfg;
    
    // Use this for initialization
	void Start () {
		
	}

    public void GenerateField()
    {
        var nBodies = rfg.GenerateField();
        nBodySimulator.nBodyObjects.AddRange(nBodies);
    }
	
	// Update is called once per frame
	void Update () {
	    
        if (Input.GetKeyDown(KeyCode.Period)) //>
        {
            nBodySimulator.dtScale *= nBodySimulator.dtScaleMultiplier;
        }
        if ( Input.GetKeyDown(KeyCode.Comma)) //<
        {
            nBodySimulator.dtScale /= nBodySimulator.dtScaleMultiplier;
        }


	}


}
