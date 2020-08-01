using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour{
    public float speed;
    public float rotSpeed;
    // Shield cooldown?? Player state?
    
    public CharacterController controller;


    // Movement debuff for player when bringing up shield
    // Should it be a flat or multiplicative debuff?
    [SerializeField]
    private float shieldMvtDebuff;
    
    private Camera _cam;
    private bool _shieldActive;
    private Transform _shield;

    private void Start()
    {
        _shield = gameObject.transform.GetChild(0);
        _shieldActive = false;
        _cam = Camera.main;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = Vector3.right * x + Vector3.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space) && !_shieldActive){
            _shield.Rotate(0,0,30);
            _shieldActive = true;
            speed *= shieldMvtDebuff;
        }
        if (Input.GetKeyUp(KeyCode.Space) && _shieldActive){
            _shield.Rotate(0,0,-30);
            _shieldActive = false;
            speed /= shieldMvtDebuff;
        }

        Debug.DrawLine(transform.position, transform.position + transform.forward , Color.red);
        Vector3 mousePos = Input.mousePosition;
        Ray mouseCast = _cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(mouseCast, out var hit, 200))
        {
            Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Debug.DrawLine(transform.position, targetPos, Color.blue);
 
            // We need some distance margin with our movement
            // or else the character could twitch back and forth with slight movement
            if (Vector3.Distance(targetPos, transform.position) >= 2.5f){
                var q = Quaternion.LookRotation(targetPos - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotSpeed * Time.deltaTime);
            }
        }

    }
}
