using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truckController : MonoBehaviour
{
    public List<GameObject> golds;
    public GameObject goldsParent;
    private int currentGold; // arabada açýk olan altýnlarýn sayýsý




    // Start is called before the first frame update
    void Start()
    {
        golds = new List<GameObject>();
        foreach (Transform gold in goldsParent.transform)
        {
            golds.Add(gold.gameObject);
            gold.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player") return;
        var player = other.gameObject.GetComponent<playerController>();

        var gold = player.LoadGoldToTruck();
        currentGold += gold;

        for (int i = 0; i < currentGold; i++) 
        {
            golds[i].gameObject.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
