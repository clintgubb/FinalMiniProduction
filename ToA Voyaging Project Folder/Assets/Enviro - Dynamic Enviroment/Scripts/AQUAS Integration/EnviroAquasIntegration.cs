#if AQUAS_PRESENT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Enviro/Integration/AQUAS Integration")]
public class EnviroAquasIntegration : MonoBehaviour {

	[Header("AQUAS Water Plane")]
	public GameObject waterObject;

	[Header("Setup")]
	[Range(1f,10f)]
	public float waterFogOffset = 3f;
	[Range(-1000,1000)]
	public int renderQueue = 100;
	public bool deactivateAquasReflectionProbe = true;
	public bool deactivateEnviroFogUnderwater = true;

	[Header("Settings")]
	[Range(0f,1f)]
	public float underwaterFogColorInfluence = 0.3f;
	//
	private GameObject enviroWaterDepth;
	private AQUAS_LensEffects aquasUnderWater;
	private bool isUnderWater;
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

		if(GameObject.Find ("UnderWaterCameraEffects") != null)
			aquasUnderWater = GameObject.Find ("UnderWaterCameraEffects").GetComponent<AQUAS_LensEffects> ();
	
		SetupEnviroWithAQUAS ();
	}

	void Update () 
	{
		if (myRenderer != null)
			myRenderer.material.renderQueue = defaultRenderQueue + renderQueue;

		//Check if we are underwater! Deactivate the workaround plane and enviro fog.
		if (waterObject != null && aquasUnderWater != null) {
			if (aquasUnderWater.underWater && !isUnderWater) {
				enviroWaterDepth.SetActive (false);
				if (deactivateEnviroFogUnderwater) {
					EnviroSky.instance.Fog.AdvancedFog = false;
					EnviroSky.instance.customFogIntensity = underwaterFogColorInfluence;
				}
				EnviroSky.instance.updateFogDensity = false;
				isUnderWater = true;
			} else if (!aquasUnderWater.underWater && isUnderWater) {
				enviroWaterDepth.SetActive (true);
				if (deactivateEnviroFogUnderwater) {
					EnviroSky.instance.updateFogDensity = true;
					EnviroSky.instance.Fog.AdvancedFog = true;
					RenderSettings.fogDensity = EnviroSky.instance.Weather.currentActiveWeatherPreset.fogDensity;
					EnviroSky.instance.customFogColor = aquasUnderWater.underWaterParameters.fogColor;
					EnviroSky.instance.customFogIntensity = 0f;
				}
				isUnderWater = false;
			}
		}
	}

	public void SetupEnviroWithAQUAS ()
	{
		if (waterObject != null) {
			CreateWaterFixObject (waterObject);

			if (deactivateAquasReflectionProbe)
				DeactivateReflectionProbe (waterObject);

			if (EnviroSky.instance.Fog.AdvancedFog == false)
				deactivateEnviroFogUnderwater = false;

			if (aquasUnderWater != null)
				aquasUnderWater.setAfloatFog = false;
			
			} else {
				Debug.Log ("AQUAS Object not found! This component will be disabled!");
				this.enabled = false;
				return;
			}
	}


	private void DeactivateReflectionProbe (GameObject aquas)
	{
		GameObject probe = GameObject.Find (aquas.name + "/Reflection Probe");
		if (probe != null)
			probe.GetComponent<ReflectionProbe> ().enabled = false;
		else
		Debug.Log ("Cannot find AQUAS Reflection Probe!");
	}

	public void CreateWaterFixObject (GameObject aquas)
	{
		MeshFilter auqasMeshFilter;
		Mesh auqasMesh = null;
	
		// Get AQUAS Mesh
		if (aquas != null) {
			auqasMeshFilter = aquas.GetComponent<MeshFilter> ();
			if (auqasMeshFilter != null) {
				auqasMesh = auqasMeshFilter.mesh;
			} else {
				Debug.Log ("Could not get AQUAS MeshFilter!");
				return;
			}
		}

		if (auqasMesh == null) {
			Debug.Log ("Could not get AQUAS Mesh!");
			return;
		}

		//Create Fog fix object and material
		GameObject dp = new GameObject ();
		dp.name = "Enviro with AQUAS";
		myRenderer = dp.AddComponent<MeshRenderer> ();
		MeshFilter mF = dp.AddComponent<MeshFilter> ();
		Material mat = new Material (Shader.Find ("Enviro/Depth"));
		myRenderer.material = mat;
		mF.mesh = auqasMesh;
		defaultRenderQueue = mat.renderQueue;
		myRenderer.receiveShadows = false;
		myRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		myRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		dp.transform.position = new Vector3 (aquas.transform.position.x, aquas.transform.position.y - waterFogOffset, aquas.transform.position.z);
		dp.transform.localScale = aquas.transform.localScale;
		enviroWaterDepth = dp;
	}
}
#endif