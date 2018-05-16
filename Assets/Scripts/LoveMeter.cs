﻿using UnityEngine;
using UnityEngine.UI;

public class LoveMeter : MonoBehaviour {

    private Slider slider;

	// Use this for initialization
	void Start () {
        this.slider = this.GetComponentInChildren<Slider>();
	}

    public void UpdateLove(float amount)
    {
        this.slider.value += amount;
    }
}
