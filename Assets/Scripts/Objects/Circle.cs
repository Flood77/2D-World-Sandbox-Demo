﻿using UnityEngine;


public struct Circle
{
    public Vector2 center;
    public float radius;

    public Circle(Vector2 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    //Check for overlap with a point
    public bool Contains(Vector2 point)
    {
        Vector2 direction = center - point;
        float sqrDistance = direction.sqrMagnitude;
        float sqrRadius = (radius * radius);

        return (sqrDistance <= sqrRadius);
    }

    //Check for overlap with another object
    public bool Contains(Circle circle)
    {
        Vector2 direction = center - circle.center;
        float sqrDistance = direction.sqrMagnitude;
        float sqrRadius = ((radius + circle.radius) * (radius + circle.radius));

        return (sqrDistance <= sqrRadius);
    }
}

