using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    /*
        This script is used for adjusting the planets orbit and rotation speed
        This is because we set the speeds as acurate as we can to real life values 
        Even tho these values are extremly scaled down, the model is scaled down even more
        So we just use a script so we can manage all these speeds at once
    */
    
    public PlanetOrbit[] orbits; //Array to hold all Planet Orbit scripts
    public PlanetSpin[] spins;  //Array to hold all Planet Spin scripts

    [Range(0f, 1f)] public float globalSpeedMultiplier = 1f; //Multiplier

    private float[] originalOrbitSpeeds; //Stores original orbit speeds
    private Vector3[] originalSpinSpeeds; //Stores original spin speeds

    void Start()
    {
        //Cache original orbit speeds
        originalOrbitSpeeds = new float[orbits.Length];
        for (int i = 0; i < orbits.Length; i++)
        {
            if (orbits[i] != null)
            {
                //We preserve the original speed of the planets orbit
                originalOrbitSpeeds[i] = orbits[i].speed;
            }
        }

        //Cache original spin speeds
        originalSpinSpeeds = new Vector3[spins.Length];
        for (int i = 0; i < spins.Length; i++)
        {
            if (spins[i] != null)
            {
                //We preserve the original speed of the planets rotation
                originalSpinSpeeds[i] = spins[i].rotationSpeed;
            }
        }
    }

    void Update()
    {
        //Adjusting orbit speeds based on the multiplier
        for (int i = 0; i < orbits.Length; i++)
        {
            if (orbits[i] != null)
            {
                //Scales proportionate to the multiplier
                orbits[i].speed = originalOrbitSpeeds[i] * globalSpeedMultiplier;
            }
        }

        //Adjusting spin speeds based on the global multiplier
        for (int i = 0; i < spins.Length; i++)
        {
            if (spins[i] != null)
            {
                //Scales proportionate to the multiplier
                spins[i].rotationSpeed = originalSpinSpeeds[i] * globalSpeedMultiplier;
            }
        }
    }
}
