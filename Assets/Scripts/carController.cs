using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    private float moveSpeed = 10f;


    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player") return;
        var player = other.gameObject.GetComponent<playerController>();

        player.Ragdoll(true);
        player.DropGoldFromHand();
    }






}
