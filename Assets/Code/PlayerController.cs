using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float minRotation = -60f;
    [SerializeField] private float maxRotation = 90f;

    private float _rotationY;


    private CharacterController _characterController;
    private Camera _camera;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 moveDirection = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"));

        _characterController.Move(moveDirection * speed);
        
        MouseLook();
    }
    
    private void MouseLook()
    {
        var mouseOffsetX = Input.GetAxis("Mouse X") * sensitivity;
        var mouseOffsetY = Input.GetAxis("Mouse Y") * sensitivity;

        _rotationY += mouseOffsetY;
        _rotationY = Mathf.Clamp(_rotationY, minRotation, maxRotation);
        var cameraRotation = _camera.transform.eulerAngles;
            
        _camera.transform.rotation = Quaternion.Euler(-_rotationY, cameraRotation.y, cameraRotation.z);
            
        transform.rotation *= Quaternion.Euler(Vector3.up * mouseOffsetX);
    }
}
