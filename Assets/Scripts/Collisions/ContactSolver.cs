using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach(var c in contacts)
        {
            //separation
            float totalInverseMass = c.bodyA.inverseMass + c.bodyB.inverseMass;
            Vector2 separation = c.normal * c.depth / totalInverseMass;

            c.bodyA.position = c.bodyA.position + separation * c.bodyA.inverseMass;
            c.bodyB.position = c.bodyB.position - separation * c.bodyB.inverseMass;

            //collision impulse
            Vector2 relativeVelocity = c.bodyA.velocity - c.bodyB.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, c.normal);

            if (normalVelocity > 0) continue;

            float restitution = (c.bodyA.restitution + c.bodyB.restitution) * 0.5f;
            float impulseMagnitude = -(1.0f + restitution) * normalVelocity / totalInverseMass;

            Vector2 impulse = c.normal * impulseMagnitude;
            c.bodyA.AddForce(c.bodyA.velocity + (impulse * c.bodyA.inverseMass), Body.eForceMode.Velocity);
            c.bodyB.AddForce(c.bodyB.velocity - (impulse * c.bodyB.inverseMass), Body.eForceMode.Velocity);
        }
    }
}
