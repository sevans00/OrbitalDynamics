using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodyObject : MonoBehaviour {

    public Vector3 force;
    public Vector3 velocity;
    public float mass;
    public bool toBeMerged = false; //Utility for NBodySimulator
    public bool hasBeenMerged = false;
    public List<NBodyObject> mergeList = new List<NBodyObject>();
    public float radius = 1f;


    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	public void DoUpdate (float dt)
    {
        //transform.localScale = mass * Vector3.one;
        UpdateVolume();
        velocity += dt * force / mass;
        transform.position += dt * velocity;
        force = Vector3.zero;
        Debug.DrawLine(transform.position + Vector3.back * radius, transform.position + Vector3.back * radius + velocity * dt * 20, Color.red);
    }

    
    void OnPostRender()
    {
        GLDrawLine(transform.position, transform.position + velocity * 100000 * 20);
    }

    //This is dumb and i don't like it 'cause it doesn't work
    public Material mat;
    private void GLDrawLine(Vector3 start, Vector3 end)
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadIdentity();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(start);
        GL.Vertex(end);
        GL.End();
        GL.PopMatrix();
    }
    //*/

    public void UpdateVolume()
    {
        radius = Mathf.Pow((3f * (mass)) / (4f * Mathf.PI), 1f / 3f);
        transform.localScale = radius * Vector3.one;
    }

    internal void AddBodyForce(NBodyObject b, float G)
    {
        var a = this;
        //float EPS = 3e4f;      // softening parameter (just to avoid infinities)
        var direction = b.transform.position - a.transform.position;
        var distance = Vector3.Distance(a.transform.position, b.transform.position);
        float F = (G * a.mass * b.mass) / (distance*distance);
        a.force = F * (direction / distance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + velocity*1000);
    }
}
