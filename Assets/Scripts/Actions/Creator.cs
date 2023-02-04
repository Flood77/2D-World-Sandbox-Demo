using UnityEngine;

public class Creator : Action
{
    [SerializeField] private GameObject original;
    [SerializeField] private FloatData speed;
    [SerializeField] private FloatData damping;
    [SerializeField] private FloatData size;
    [SerializeField] private FloatData density;
    [SerializeField] private FloatData restitution;
    [SerializeField] private  BodyEnumData bodyType;

    protected override eActionType actionType => eActionType.Creator;

    private bool action = false;
    private bool single = false;

    //Start Click Action
    public override void StartAction()
    {
        action = true;
        single = true;
    }
    //Stop Click Action
    public override void StopAction()
    {
        action = false;
    }

    //Create object on click
    void Update()
    {
        //Check for single use click or ctrl dump
        if (action && (single || Input.GetKey(KeyCode.LeftControl)))
        {
            single = false;

            //Find mouse position
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Create object at mouse position 
            GameObject gameObject = Instantiate(original, position, Quaternion.identity);

            //If object's body exist, fill in slider variables
            if (gameObject.TryGetComponent<Body>(out Body body))
            {
                body.shape.size = size;
                body.damping = damping;
                body.shape.density = density;
                body.restitution = restitution;
                body.type = (Body.eType)bodyType.value;
                Vector2 force = UnityEngine.Random.insideUnitSphere.normalized * speed;
                body.AddForce(force, Body.eForceMode.Velocity);
                World.Instance.bodies.Add(body);
            } 
        }
    }
}
