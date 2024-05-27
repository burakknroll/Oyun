using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    private Animator animator; // animasyon iþlemleri için animatoru tanýmladýk
    public List<GameObject> goldList;
    public int carry;

    public float reduceSpeed = 0.5f;
    private float baseMovementSpeed;

    public int CarryLimit => goldList.Count; // taþýma limitim

    public Transform boneParent;
    public bool CanMove = true;
    public Transform spinePosition;

    private void Start()
    {
        baseMovementSpeed = movementSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // animatore ulaþtýk

        Ragdoll(false);

    }

    void Update()
    {
        if (CanMove == false) return;

        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movementDirection = new Vector3(horizontal, 0, vertical);


        // animator.SetBool("isRunning", rb.velocity != Vector3.zero); ikisi de olur
        animator.SetBool("isRunning", movementDirection != Vector3.zero);
        animator.SetBool("isCarrying", carry != 0);

        if (movementDirection == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            return;
        }



        rb.velocity = movementDirection * movementSpeed;

        var rotationDirection = Quaternion.LookRotation(movementDirection); //movement direction yönünü rotation olarak kaydet
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);
    }

    public bool CollectGold()
    {
        if (carry == CarryLimit) return false;
        goldList[carry].gameObject.SetActive(true);
        carry++;

        movementSpeed -= reduceSpeed;

        return true;
    }

    public int DropGoldFromHand()
    {
        var carryingGold = carry;
        if (carryingGold == 0) return 0;

        foreach (var gold in goldList)
            gold.SetActive(false);

        carry = 0;
        movementSpeed = baseMovementSpeed;
        return carryingGold;
    }

    public void Ragdoll(bool isActive)
    {
        animator.enabled = !isActive;
        var colliders = boneParent.GetComponentsInChildren<Collider>();
        var rigidbodies = boneParent.GetComponentsInChildren<Rigidbody>();

        foreach (var coll in colliders)
            coll.enabled = isActive;

        foreach (var rig in rigidbodies)
            rig.isKinematic = !isActive;

        GetComponent<Collider>().enabled = !isActive;
        CanMove = !isActive;

        if (!isActive)
        {
            StartCoroutine(CloseRagdoll());
        }

    }

    public IEnumerator CloseRagdoll()
    {
        yield return new WaitForSeconds(3f);
        Ragdoll(false);
        transform.position = new Vector3(spinePosition.position.x, 0, spinePosition.position.z);

    }

}
