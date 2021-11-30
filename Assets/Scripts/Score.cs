using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private Text textComponent;
    public MainCharacter mainCharacter;

    // Use this for initialization
    void Start () {
        textComponent = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        textComponent.text = "Score: " + mainCharacter.scoreCount.ToString();
	}
}
