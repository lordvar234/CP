using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class RotationMoseStatus : MonoBehaviour
{
    public float Offset;

    public double gradus;

    void Update()
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(TargetPos.y, TargetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + Offset);

    }
}
