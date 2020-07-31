using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour{
    public float speed;
    // Shield cooldown?? Player state?
    
    public CharacterController controller;
    
    private Camera cam;
    private bool shieldActive;
    private Transform shield;

    private void Start()
    {
        shield = gameObject.transform.GetChild(0);
        shieldActive = false;
        cam = Camera.main;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = Vector3.right * x + Vector3.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space) && !shieldActive){
            shield.Rotate(0,0,30);
            shieldActive = true;
            speed /= 2;
        }
        if (Input.GetKeyUp(KeyCode.Space) && shieldActive){
            shield.Rotate(0,0,-30);
            shieldActive = false;
            speed *= 2;
        }
        float yDelta = cam.transform.position.y - gameObject.transform.position.y;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, yDelta);
        transform.LookAt(cam.ScreenToWorldPoint(mousePos));
    }
}
