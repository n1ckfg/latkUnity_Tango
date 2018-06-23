using UnityEngine;
using System.Collections;

public class WebcamParticles : MonoBehaviour {

	public Renderer ren;
	public ParticleSystem particleSystem;
	public TextureToParticles textureToParticles;
	public WebcamPhoto webcamPhoto;
	public float hideDelay = 4f;

	private float markTime = 0f;
	private ParticleRenderer particleRen;

	void Awake() {
		particleRen = particleSystem.GetComponent<ParticleRenderer>();
	}

	void Start() {
		ren.enabled = true;
		particleRen.enabled = false;
	}

	void Update() {
		if (webcamPhoto.photoTaken) {
			markTime = Time.realtimeSinceStartup;
			ren.enabled = false;
			particleRen.enabled = true;

			textureToParticles.texture = webcamPhoto.photo;
			textureToParticles.generateParticles();
		}

		if (!ren.enabled && Time.realtimeSinceStartup > markTime + hideDelay) {
			ren.enabled = true;
			particleRen.enabled = false;
		}
	}

}
