using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalPeriodCalculator : MonoBehaviour {

    public float orbitalTime;
    public bool started = false;

    public NBodyObject orbitee;
    public NBodyObject orbiter;

    public float startAngle;
    public float currentAngle;
    public float angleDifference;
    public float startTime;

	// Use this for initialization
	void Start () {
        startAngle = getOrbitAngle();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (!started)
        {
            started = true;
            startTime = Time.time;
            return;
        }

        currentAngle = getOrbitAngle();
        if (currentAngle > startAngle)
            angleDifference = currentAngle - startAngle;
        else
            angleDifference = startAngle - currentAngle;
        if ( angleDifference <= 5f)
        {
            started = false;
            orbitalTime = Time.time - startTime;
        }

    }

    private float getOrbitAngle()
    {
        return Vector3.SignedAngle(
                        orbitee.transform.position - orbiter.transform.position,
                        Vector3.right, Vector3.forward
                        );
    }
}
