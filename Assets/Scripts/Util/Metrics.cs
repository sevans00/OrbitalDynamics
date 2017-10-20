using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metrics : MonoBehaviour {

    public NBodySimulator simulator;
    

    public Vector3 startingEnergy;
    public Vector3 currentEnergy;
    public float startingTotalEnergy = 0f;
    public float currentTotalEnergy = 0f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (startingEnergy.magnitude == 0)
        {
            startingEnergy = getTotalEnergy();
            startingTotalEnergy = startingEnergy.magnitude;
        }
        currentEnergy = getTotalEnergy();
        currentTotalEnergy = currentEnergy.magnitude;

        Debug.DrawLine(Vector3.zero, currentEnergy * 1e10f, Color.green);
	}

    public Vector3 getTotalEnergy()
    {
        var totalEnergyX = 0f;
        var totalEnergyY = 0f;
        var totalEnergyZ = 0f;
        foreach( var nBody in simulator.nBodyObjects )
        {
            totalEnergyX += nBody.mass *( nBody.velocity.x * nBody.velocity.x) / 2;
            totalEnergyY += nBody.mass * (nBody.velocity.y * nBody.velocity.y) / 2;
            totalEnergyZ += nBody.mass * (nBody.velocity.z * nBody.velocity.z) / 2;
        }
        return new Vector3(totalEnergyX, totalEnergyY, totalEnergyZ);
    }
}
