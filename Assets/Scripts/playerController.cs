using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movementDirection = new Vector3(horizontal, 0, vertical);

        if(movementDirection == Vector3.zero)
        {
            Debug.Log("Su an input yok");
            return;
        }

        rb.velocity = movementDirection * movementSpeed;

        var rotationDirection = Quaternion.LookRotation(movementDirection); //movement direction yönünü rotation olarak kaydet
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime); 
    }
}
