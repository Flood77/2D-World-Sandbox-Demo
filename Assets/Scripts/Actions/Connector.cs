using UnityEngine;

public class Connector : Action
{
	[SerializeField] private BoolData distanceLength;
	[SerializeField] private FloatData springK;
	[SerializeField] private FloatData springLength;

	protected override eActionType actionType => eActionType.Connector;

	private Body source { get; set; } = null;

	//Find and Store selected object
	public override void StartAction()
	{
		Body body = GetBodyFromPosition(Input.mousePosition);
		if (body != null)
		{
			source = body;
		}
	}

	//Find and Connect secondary object
	public override void StopAction()
	{
		if (source != null)
		{
			Body destination = GetBodyFromPosition(Input.mousePosition);
			if (destination != null && destination != source)
			{
				//Find distance between given objects
				float restLength = distanceLength ? (source.position - destination.position).magnitude : springLength;
				Create(source, destination, restLength, springK);
			}
		}

		source = null;
	}

	//Drawline a line from selected object to mouse point
	void Update()
	{
		if (source != null)
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Lines.Instance.AddLine(source.position, position, Color.white, 0.1f);
		}
	}

	//Creates a new spring to connect given objects
	void Create(Body bodyA, Body bodyB, float restLength, float k)
	{
		Spring spring = new Spring();
		spring.bodyA = bodyA;
		spring.bodyB = bodyB;
		spring.restLength = restLength;
		spring.k = k;

		World.Instance.springs.Add(spring);
	}

    //Returns selected body based on raycast
    private static Body GetBodyFromPosition(Vector2 position)
    {
        Body body = null;

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider)
        {
            body = hit.collider.gameObject.GetComponent<Body>();
        }

        return body;
    }
}
