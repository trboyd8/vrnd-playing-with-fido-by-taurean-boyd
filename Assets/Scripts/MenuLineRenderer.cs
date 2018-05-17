using UnityEngine;

public class MenuLineRenderer : MonoBehaviour {

    private LineRenderer lineRenderer;
    public float lineLength;

	// Use this for initialization
	void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position + this.transform.forward * lineLength);
	}
}
