using System;
using DG.Tweening;
using UnityEngine;

public class BackgroundMovment : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private SpriteRenderer _renderer;
    
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        _renderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
