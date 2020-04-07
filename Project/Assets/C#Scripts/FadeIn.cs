using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public float fadeTime;

    private Image levelTransition;

	// Use this for initialization
	void Start ()
    {
        levelTransition = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        levelTransition.CrossFadeAlpha(0f, fadeTime, false);

        if(levelTransition.color.a == 0)
        {
            gameObject.SetActive(false);
        }
	}
}
