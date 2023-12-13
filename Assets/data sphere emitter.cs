using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSphereEmitter : MonoBehaviour
{
    public ParticleSystem system;

    public int numberOfStars;

    public float minSize = 0.1f; // Minimum size for the particles
    public float maxSize = 1.0f; // Maximum size for the particles
    public float minFlickerSpeed = 1.0f; // Minimum speed of flickering
    public float maxFlickerSpeed = 5.0f; // Maximum speed of flickering
    public float flickerSpeed = 5.0f; // Speed of flickering

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private float[] originalSizes;
    private float[] flickerSpeeds;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        originalSizes = new float[particleSystem.main.maxParticles];
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        flickerSpeeds = new float[particleSystem.main.maxParticles];
        for(int i=0; i<numberOfStars; i++){
            system.Emit(new ParticleSystem.EmitParams() { position = Random.insideUnitSphere }, 1); 
            flickerSpeeds[i] = Random.Range(minFlickerSpeed, maxFlickerSpeed);
        }   
    }

    // Update is called once per frame
    void Update(){
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            if (particles[i].remainingLifetime > 0.01f)
            {
                float flickerValue = Mathf.PingPong(Time.time * flickerSpeeds[i], 1.0f);
                float targetSize = Mathf.Lerp(minSize, maxSize, flickerValue);

                particles[i].startSize = targetSize;
            }
            else if (originalSizes[i] != 0f)
            {
                particles[i].startSize = originalSizes[i];
                originalSizes[i] = 0f;
            }
        }

        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}
