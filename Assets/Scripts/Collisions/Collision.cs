using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collision
{
    public static void CreateContacts(List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();

        for (int i = 0; i < bodies.Count - 1; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                if (bodyA.type == Body.eType.Static && bodyB.type == Body.eType.Static) continue;

                Circle a = new Circle(bodyA.position, ((CircleShape)bodyA.shape).radius);
                Circle b = new Circle(bodyB.position, ((CircleShape)bodyB.shape).radius);

                if (a.Contains(b))
                {
                    Contact contact = new Contact() { bodyA = bodyA, bodyB = bodyB };

                    float distance = (a.center - b.center).magnitude;
                    contact.depth = (a.radius + b.radius) - distance;

                    Vector2 v = a.center - b.center;
                    contact.normal = v.normalized; 

                    contacts.Add(contact);
                }
            }
        }
    }
}