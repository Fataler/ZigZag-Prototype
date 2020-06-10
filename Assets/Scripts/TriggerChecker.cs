using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public Rigidbody rb;

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = Vector3.down * 5f;
            Invoke("SelfDestroyParent", 0.5f);
            GameController.instance.RiseScore();
        }
    }

    private void SelfDestroyParent()
    {
        Transform parent = transform.parent;
        Destroy(parent.gameObject, 3f);
    }
}