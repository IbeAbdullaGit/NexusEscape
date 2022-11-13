using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace FlyweightPattern{
    public class FlyWeightParticles : MonoBehaviour
    {
        ParticleSystem ps;
        public float strength = 9.23f;
        public float freq = 2.5f;

        public GameObject prefab;
        public GameObject newParticle;
        public GameObject ObjectWParts;
        // Start is called before the first frame update
        void Start()
        {
            ps = prefab.GetComponent<ParticleSystem>();
            
           
          
            ParticleAdjustment();
        }

        // Update is called once per frame
        void ParticleAdjustment()
        {
            var noise = ps.noise;
            //noise.enabled = true; 
            for (int i = 0; i < 8; i++)
            {

                newParticle = Instantiate(prefab);
                ps = newParticle.GetComponent<ParticleSystem>();


                //Without flyweight
                //noise.frequency = Random.Range(0f, 5f);
                //noise.strength = Random.Range(2f, 10f);
                // newParticle.transform.position = newParticle.transform.position + new Vector3(Random.Range(0f, 100f), Random.Range(0f, 50f), 0);

                //With flyweight
                noise.strength = strength;
                noise.frequency = freq;
                newParticle.transform.position = newParticle.transform.position + new Vector3(Random.Range(0f, 5f), Random.Range(0f, 10f), 0);

            }
        }
    }

}

