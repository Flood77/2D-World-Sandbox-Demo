using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public FloatData gravity;
    public FloatData fixedFPS;
    public StringData FPS;
    public FloatData gravitation;

    private float timeAccumulator = 0; 
    public float fixedDeltaTime { get { return (1.0f / fixedFPS.data); } }
    private Vector2 size;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.data); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    void Update()
    {
        FPS.data = (1.0f / Time.deltaTime).ToString();

        if (!simulate.data) return;

        GravitationalForce.ApplyForce(bodies, gravitation.data);

        float dt = Time.deltaTime;
        timeAccumulator += dt;

        while(timeAccumulator >= fixedDeltaTime)
        {

            bodies.ForEach(body => body.Step(dt));
            bodies.ForEach(body => Intregated.SemiImplicitEuler(body, dt));

            bodies.ForEach(body => body.shape.color = Color.white);

            if (collision)
            {
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(c => { c.bodyA.shape.color = Color.red; c.bodyB.shape.color = Color.red; });
                ContactSolver.Resolve(contacts);
            }

            timeAccumulator -= fixedDeltaTime;
        }

        if (wrap)
        {
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
        
    }
}
