using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData fixedFPS;
    public StringData FPS;

    private float timeAccumulator = 0; 
    public float fixedDeltaTime { get { return (1.0f / fixedFPS.data); } }

    static World instance;
    static public World Instance { get { return instance; } }

    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        FPS.data = (1.0f / Time.deltaTime).ToString();
        if (!simulate.data) return;

        float dt = Time.deltaTime;
        timeAccumulator += dt;

        while(timeAccumulator > fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(dt));
            bodies.ForEach(body => Intregated.SemiImplicitEuler(body, dt));

            timeAccumulator -= fixedDeltaTime;
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
        
    }
}
