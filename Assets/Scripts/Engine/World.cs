using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private BoolData simulate;
    [SerializeField] private BoolData collision;
    [SerializeField] private BoolData wrap;
    [SerializeField] private FloatData gravity;
    [SerializeField] private FloatData fixedFPS;
    [SerializeField] private StringData FPS;
    [SerializeField] private FloatData gravitation;

    private float timeAccumulator = 0;
    private float fixedDeltaTime { get { return (1.0f / fixedFPS.data); } }

    private Vector2 size; 
    public Vector2 Gravity { get { return new Vector2(0, gravity.data); } }

    static World instance;
    static public World Instance { get { return instance; } }

    public List<Spring> springs { get; set; } = new List<Spring>();
    public List<Body> bodies { get; set; } = new List<Body>();

    //Set instance and screen size
    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    void Update()
    {
        //Supply FPS data
        FPS.data = (1.0f / Time.deltaTime).ToString();

        //Draw springs
        springs.ForEach(spring => spring.Draw());

        //Return if not simulating
        if (!simulate.data) return;

        //Apply Gravitational Forces
        ApplyGravitationalForce(bodies, gravitation.data);
        springs.ForEach(spring => spring.ApplyForce());

        //Add to timer
        float dt = Time.deltaTime;
        timeAccumulator += dt;

        while(timeAccumulator >= fixedDeltaTime)
        {

            //Set body variables
            bodies.ForEach(body => body.Step());
            bodies.ForEach(body => setForce(body, dt));

            //Reset body color, then color if in contact with another
            bodies.ForEach(body => body.shape.color = Color.white);
            if (collision) { CollisionResolution(); }

            timeAccumulator -= fixedDeltaTime;
        }

        //Keeps the bodies within boundary if checked
        if (wrap) { bodies.ForEach(body => body.position = Wrap(body.position, -size, size)); }

        //Reset body force variables
        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }

    //Resolve Object Collisions
    private void CollisionResolution()
    {
        List<Contact> contacts = new List<Contact>();

        //Find all collisions
        for (int i = 0; i < bodies.Count - 1; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                //Check each object pair for overlap
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                if (bodyA.type == Body.eType.Static && bodyB.type == Body.eType.Static) continue;

                Circle a = new Circle(bodyA.position, ((CircleShape)bodyA.shape).radius);
                Circle b = new Circle(bodyB.position, ((CircleShape)bodyB.shape).radius);

                //If colliding store the collsion
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

        //Color all colliding objects
        contacts.ForEach(c => { c.bodyA.shape.color = Color.red; c.bodyB.shape.color = Color.red; });

        //Deal with collision movements
        foreach (var c in contacts)
        {
            //Separate the objects, so they don't overlap
            float totalInverseMass = c.bodyA.inverseMass + c.bodyB.inverseMass;
            Vector2 separation = c.normal * c.depth / totalInverseMass;

            c.bodyA.position = c.bodyA.position + separation * c.bodyA.inverseMass;
            c.bodyB.position = c.bodyB.position - separation * c.bodyB.inverseMass;

            //Calculate Collision Velocity
            Vector2 relativeVelocity = c.bodyA.velocity - c.bodyB.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, c.normal);

            if (normalVelocity > 0) continue;

            //Calculate Collision Impulse
            float restitution = (c.bodyA.restitution + c.bodyB.restitution) * 0.5f;
            float impulseMagnitude = -(1.0f + restitution) * normalVelocity / totalInverseMass;

            Vector2 impulse = c.normal * impulseMagnitude;
            c.bodyA.AddForce(c.bodyA.velocity + (impulse * c.bodyA.inverseMass), Body.eForceMode.Velocity);
            c.bodyB.AddForce(c.bodyB.velocity - (impulse * c.bodyB.inverseMass), Body.eForceMode.Velocity);
        }
    }

    //Set body's force & position variables
    private void setForce(Body body, float dt)
    {
        body.velocity += (body.acceleration * dt);
        body.position += (body.velocity * dt);
        body.velocity *= (1f / (1f + (body.damping * dt)));
    }

    //Apply gravitational force between given bodies
    private static void ApplyGravitationalForce(List<Body> bodies, float G)
    {
        for (int i = 0; i < bodies.Count - 1; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                Vector2 direction = bodyA.position - bodyB.position;
                float distanceSqr = Mathf.Max(direction.sqrMagnitude, 1);

                float force = G * ((bodyA.mass * bodyB.mass) / distanceSqr);

                bodyA.AddForce(-direction.normalized * force, Body.eForceMode.Force);
                bodyB.AddForce(direction.normalized * force, Body.eForceMode.Force);
            }
        }
    }

    //Wrap point back to min, upon hitting max, and vice versa
    private static Vector2 Wrap(Vector2 point, Vector2 min, Vector2 max)
    {
        if (point.x > max.x) point.x = min.x;
        if (point.y > max.y) point.y = min.y;
        if (point.x < min.x) point.x = max.x;
        if (point.y < min.y) point.y = max.y;

        return point;
    }
}
