using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BallControl : MonoBehaviour {
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timerText;

    private Rigidbody rb;
    private int count;
    bool finished = false;
    private float t;
    private float startTime;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winTextObject.SetActive(false);
        startTime = Time.time;
        finished = false;
    }
    private void OnMove (InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void setCountText() {
        countText.text = "Score: " + count.ToString() + "/12";
        /*if (count >= 12) {
            winTextObject.SetActive(true);
        }*/
    }
    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void Update() {
        if (finished == false) {
            t = Time.time - startTime;
        }
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds; 
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        } 
        if (other.gameObject.name == "Finish Line") {
            winTextObject.SetActive(true);
            finished = true;
        }
    }
}