using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InfoScreen : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private RectTransform Pos;
    public GameObject info;
        void Start()
    {
        Pos = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }
    void Update()
    {
        if(Math.Abs(Pos.position.x - mainCamera.ScreenToWorldPoint(Input.mousePosition).x) <= 1 && Math.Abs(Pos.position.y - mainCamera.ScreenToWorldPoint(Input.mousePosition).y) <= 1 && Input.GetMouseButton(2))
        {
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
    }
}
