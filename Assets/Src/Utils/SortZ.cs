using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortZ : MonoBehaviour
{
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spr != null && Camera.main != null)
        {
            spr.sortingOrder = (int)Camera.main.WorldToScreenPoint(spr.bounds.min).y * -1;
        }
    }

}