using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform objToFollow;
    private Vector3 offset;
    public float lerpRate = 2f;

    private void Start()
    {
        if (objToFollow == null)
        {
            objToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = objToFollow.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameController.instance.GameOver)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = objToFollow.position - offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate + Time.deltaTime);
        transform.position = pos;
    }
}