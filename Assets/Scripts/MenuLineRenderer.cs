using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLineRenderer : MonoBehaviour {

    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = this.GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position + this.transform.forward * 3.47f);
	}
}
