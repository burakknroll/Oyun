using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableController : MonoBehaviour
{
    public GameObject goldObject;
    public bool IsGoldCollectable => goldObject.activeSelf;


    private void OnCollisionEnter(Collision other)
    {
        if (!IsGoldCollectable) return;

        if (other.gameObject.tag != "Player") return;
        var player = other.gameObject.GetComponent<playerController>();

        if(player.CollectGold())
        {
            goldObject.SetActive(false);
            Invoke(nameof(ReloadGold), Random.Range(5f,15f));
        }

    }

    private void ReloadGold()
    {
        goldObject.SetActive(true); // altýn objesini aç
    }

  

}
