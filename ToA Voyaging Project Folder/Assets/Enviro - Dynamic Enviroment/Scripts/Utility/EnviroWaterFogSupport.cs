using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Enviro/Utility/Water Fog Support")]
public class EnviroWaterFogSupport : MonoBehaviour {

	[Header("Water Plane")]
	public GameObject waterObject;
	public Mesh waterPlaneMesh;
	[Header("Settings")]
	[Range(0.1f,10f)]
	public float waterFogOffset = 3f;
	[Range(-1000,1000)]
	public int renderQueue = 0;
	[Range(0f,5f)]
	public float scaleModificator = 1f;
	//
	private int defaultRenderQueue;
	private MeshRenderer myRenderer;
	//

	void Start () 
	{
		if (EnviroSky.instance == null) 
		{
			Debug.Log ("No EnviroSky in scene! This component will be disabled!");
			this.enabled = false;
			return;
		}
		SetupWaterFog ();
	}

	public void SetupWaterFog ()
	{
		if (waterObject != null) {
			CreateWaterFixObject (waterObject);
			} else {
				Debug.Log ("Water Object not found! This component will be disabled!");
				this.enabled = false;
				return;
			}
	}
		

	void Update ()
	{
		if (myRenderer != null)
			myRenderer.material.renderQueue = defaultRenderQueue + renderQueue;
	}

	private Mesh GetWaterMesh (GameObject water)
	{
		MeshFilter meshFilter;
		Mesh mesh = null;

		meshFilter = water.GetComponent<MeshFilter> ();

		if (meshFilter != null) {
			mesh = meshFilter.mesh;
		} 
		else
		{
			meshFilter = water.GetComponentInChildren<MeshFilter> ();
			if (meshFilter != null) {
				mesh = meshFilter.mesh;
			}
		}
			
		if(!meshFilter) {
			Debug.Log ("Could not get MeshFilter!");
			return null;
		}

		return mesh;
	}

	public void CreateWaterFixObject (GameObject water)
	{
		Mesh mesh = waterPlaneMesh;
	
		// Get Mesh
		if (water != null && mesh == null)
			mesh = GetWaterMesh (water);

		if (mesh == null) 
		{
			Debug.Log ("Could not find Mesh!");
			return;
		}

		//Create Fog fix object and material
		GameObject dp = new GameObject ();
		dp.name = "Enviro Water Fog Support";
		myRenderer = dp.AddComponent<MeshRenderer> ();
		MeshFilter mF = dp.AddComponent<MeshFilter> ();
		Material mat = new Material (Shader.Find ("Enviro/Depth"));
		myRenderer.material = mat;
		defaultRenderQueue = mat.renderQueue;
		mF.mesh = mesh;
		myRenderer.receiveShadows = false;
		myRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		myRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		dp.transform.position = new Vector3 (water.transform.position.x, water.transform.position.y - waterFogOffset, water.transform.position.z);
		dp.transform.localScale = water.transform.localScale * scaleModificator;
	}
}
