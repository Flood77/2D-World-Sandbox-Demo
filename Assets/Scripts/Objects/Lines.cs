using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lines : MonoBehaviour
{
    [SerializeField] private int initialLines = 20;
    [SerializeField] private Material material;

    private static Lines instance;
    public static Lines Instance { get => instance; }

    private int numLines = 0;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    //Set Instance
    private void Awake()
    {
        instance = this;
    }

    //Draw all initial lines
	void Start()
    {
        for (int i = 0; i < initialLines; i++)
		{
            lineRenderers.Add(CreateLineRenderer());
        }
    }

    //Reset all line renderers
	private void OnEnable()
	{
        RenderPipelineManager.endCameraRendering += OnResetRender;
    }

    //Reset Renders
    private void OnResetRender(ScriptableRenderContext context, Camera camera)
    {
        Reset();
    }

    //Deactivate all lines
	public void Reset()
	{
        foreach (LineRenderer lineRenderer in lineRenderers)
		{
            lineRenderer.gameObject.SetActive(false);
        }
    }

    //Activate a line where it is needed
	public void AddLine(Vector3 start, Vector3 end, Color color, float width = 0.1f)
	{
        //Fetch first disabled line
        LineRenderer lineRenderer = GetInactiveLineRenderer();
        if (lineRenderer == null) return;

        //Activate line
        lineRenderer.gameObject.SetActive(true);

        //Set line's position
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        //Set line's width
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        //Set line's color
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    //Return a disabled line
    LineRenderer GetInactiveLineRenderer()
	{
        foreach (LineRenderer lineRenderer in lineRenderers)
		{
            if (!lineRenderer.gameObject.activeSelf)
			{
                return lineRenderer;
			}
		}

        return null;
	}

    //Create new lines
    LineRenderer CreateLineRenderer()
	{
        //Create new line with renderer
        GameObject gameObject = new GameObject();
        gameObject.transform.parent = transform;
        gameObject.AddComponent<LineRenderer>();

        //Deactive and Name line, then increment line count
        gameObject.SetActive(false);
        gameObject.name = "Line" + (numLines + 1);
        numLines++;

        //Set Line's material and layer position
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.positionCount = 2;

        return lineRenderer;
    }
}
