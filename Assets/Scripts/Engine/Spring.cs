using UnityEngine;

public class Spring : MonoBehaviour
{
    public Body bodyA { get; set; } = null;
    public Body bodyB { get; set; } = null;

    public float k { get; set; } = 20.0f;
    public float restLength { get; set; } = 0.0f;

    //Apply spring force to connected bodies
    public void ApplyForce()
    {
        Vector2 direction = bodyB.position - bodyA.position;

        float length = direction.magnitude;
        float x = length - restLength;

        Vector2 force = direction.normalized * (x * -k);

        bodyA.AddForce(-force);
        bodyB.AddForce(force);
    }

    //Draw line to represent spring
    public void Draw()
    {
        Lines.Instance.AddLine(bodyA.position, bodyB.position, Color.red, 0.1f);
    }
}
