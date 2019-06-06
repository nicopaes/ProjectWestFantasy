using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToPlay : MonoBehaviour {
    public float secondsToWait = 0;
	// Use this for initialization
	void Start () {
        StartCoroutine(waitPlay(secondsToWait));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator waitPlay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<AudioSource>().Play();
    }
}
