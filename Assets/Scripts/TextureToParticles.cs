using UnityEngine;
using System.Collections;

public class TextureToParticles : MonoBehaviour {

	public Texture2D texture;
	public float scale = 1;
	public float particleSize = 5;
	public ParticleSystem system;

	public int numberParticlesX = 64;
	public int numberParticlesY = 64;
	public bool runOnStart = false;

	void Awake() {
		if (system == null) {	
			system = GetComponent<ParticleSystem>();
		}
	}

	void Start() {
		if (runOnStart) generateParticles();
	}

	public void generateParticles() {
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numberParticlesX * numberParticlesY];
		system.maxParticles = particles.Length;
		system.Emit(particles.Length); // Creates the particles.
		system.GetParticles(particles);    // Get them so we can adjust the values
		Vector2 pixelOffset = new Vector2(texture.width / numberParticlesX, texture.height / numberParticlesY);

		int index = 0;
		Vector3 pos = Vector3.zero;
		for(int y = 0; y < numberParticlesY; ++y) {
			for (int x = 0; x < numberParticlesX; ++x) {
				// Sample the pixel value(we could do some interpolation here).          
				particles[index].position = pos;
				particles[index].color = texture.GetPixel(Mathf.RoundToInt(pixelOffset.x * x), Mathf.RoundToInt(pixelOffset.y * y));
				particles[index].size = particleSize;
				index++;
				pos.x += pixelOffset.x * scale;
			}
			pos.y += pixelOffset.y * scale;
			pos.x = 0;
		}

		system.SetParticles(particles, particles.Length);  
	}

}