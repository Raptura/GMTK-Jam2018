using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float movSpeed = 2; // units per second


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        handleInput();	
	}


    void handleInput()
    {
        if (GameManager.gm.paused)
            return;

        //ULDR Spacebar
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector2.up * Time.deltaTime * movSpeed);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector2.down * Time.deltaTime * movSpeed);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector2.left * Time.deltaTime * movSpeed);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector2.right * Time.deltaTime * movSpeed);

        //WASD E/F

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.gm.paused)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (other.gameObject.GetComponent<EvidenceBehaviour>())
            {
                EvidenceBehaviour eb = other.GetComponent<EvidenceBehaviour>();
                lookAtEvidence(eb.evidenceData);
            }
        }
    }

    void lookAtEvidence(Evidence e) {
        FindObjectOfType<Dialogue>().setupDialogue(e);
    }
}
