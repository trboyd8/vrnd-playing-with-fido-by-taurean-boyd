using UnityEngine;
using UnityEngine.UI;

public class LoveMeter : MonoBehaviour {

    private Slider slider;
    private AudioSource audioSource;
    public ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
        this.slider = this.GetComponentInChildren<Slider>();
        this.audioSource = this.GetComponent<AudioSource>();
	}

    public void UpdateLove(float amount)
    {
        this.slider.value += amount;
        if (this.slider.value == 1.0f)
        {
            this.transform.parent.transform.position = Vector3.zero;
            this.transform.parent.GetComponent<DogController>().Dance();
            this.audioSource.Play();
            this.pSystem.Play();
        }
    }
}
