using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCol : MonoBehaviour
{
    private Collider2D box1;
    public Collider2D box2;
    void Start()
    {
	    box1 = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(box1, box2);
    }
}
