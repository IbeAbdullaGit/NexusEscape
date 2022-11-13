using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace FlyweightPattern{
    public class FlyWeightParticles : MonoBehaviour
    {
        ParticleSystem.NoiseModule  noise;
        public float strength = 9.23f;
        public float freq = 2.5f;

        public GameObject prefab;
        public GameObject newParticle;
        public GameObject ObjectWParts;
        // Start is called before the first frame update
        void Start()
        {
            noise = GetComponent<ParticleSystem.NoiseModule>();
            
          
            ParticleAdjustment();
        }

        // Update is called once per frame
        void ParticleAdjustment()
        {
            for(int i = 0; i < 8; i++)
            {

                newParticle = Instantiate(prefab);
                noise = newParticle.GetComponent<ParticleSystem.NoiseModule>();


                //Without flyweight
                //noise.frequency = Random.Range(0f, 5f);
                //noise.strength = Random.Range(2f, 10f);
                // newParticle.transform.position = newParticle.transform.position + new Vector3(Random.Range(0f, 100f), Random.Range(0f, 50f), 0);

                //With flyweight
                noise.strength = strength;
                noise.frequency = freq;
                newParticle.transform.position = newParticle.transform.position + new Vector3(Random.Range(0f, 100f), Random.Range(0f, 50f), 0);

            }
        }
    }

}

