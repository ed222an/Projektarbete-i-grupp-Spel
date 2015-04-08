﻿using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour 
{
    public float followRange;
    public Transform target;
    public float speed;

    private Rigidbody2D rBody;
	// Use this for initialization
	void Start() 
    {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        if (target != null && ((transform.position.x + followRange > target.position.x && transform.position.x < target.position.x) 
            || (transform.position.x - followRange < target.position.x && transform.position.x > target.position.x)))
            FollowTarget();
	}

    void FollowTarget()
    {
        if (transform.position.x < target.position.x)
            rBody.velocity = new Vector2(1f * speed, rBody.velocity.y);
        else
            rBody.velocity = new Vector2(-1f * speed, rBody.velocity.y);
    }
}
