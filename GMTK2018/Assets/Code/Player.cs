using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float movSpeed = 2; // units per second
    public float updateDelay = 0.5f;
    public int Frame;
    public float lastChanged;
    public int lastDir;
    

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastChanged > updateDelay)
        {
            Frame += 1;
            Frame = Frame % 2;
            lastChanged = Time.time;
            if (!Input.anyKey)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                Sprite[] spritesb = Resources.LoadAll<Sprite>("player_back");
                Sprite[] spritesf = Resources.LoadAll<Sprite>("player_front");
                Sprite[] spritesr = Resources.LoadAll<Sprite>("player_right");
                Sprite[] spritesl = Resources.LoadAll<Sprite>("player_left");
                switch (lastDir)
                {
                    case 0:
                        sr.sprite = spritesb[2];
                        break;
                    case 1:
                        sr.sprite = spritesf[2];
                        break;
                    case 2:
                        sr.sprite = spritesl[2];
                        break;
                    case 3:
                        sr.sprite = spritesr[2];
                        break;
                }
            }
        }

        handleInput();	
	}


    void handleInput()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite[] spritesb = Resources.LoadAll<Sprite>("player_back");
        Sprite[] spritesf = Resources.LoadAll<Sprite>("player_front");
        Sprite[] spritesr = Resources.LoadAll<Sprite>("player_right");
        Sprite[] spritesl = Resources.LoadAll<Sprite>("player_left");
        if (GameManager.gm.paused)
            return;

        //ULDR Spacebar
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * Time.deltaTime * movSpeed);
            sr.sprite = spritesb[Frame];
            lastDir = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * Time.deltaTime * movSpeed);
            sr.sprite = spritesf[Frame];
            lastDir = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * Time.deltaTime * movSpeed);
            sr.sprite = spritesl[Frame];
            lastDir = 2;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Time.deltaTime * movSpeed);
            sr.sprite = spritesr[Frame];
            lastDir = 3;
        }

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
                FindObjectOfType<Dialogue>().setUpEvidence(eb.evidenceData); //Look at the evidence
            }

            if (other.gameObject.GetComponent<Phone>()) {
                FindObjectOfType<Dialogue>().phoneScene();
            }
        }
    }

}
