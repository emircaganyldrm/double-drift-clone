using System;
using UnityEngine;

namespace Car
{
    public class DriftVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem driftParticle;
        [SerializeField] private TrailRenderer driftTrail;

        private Vector3 _particleOffset;
        private Vector3 _trailOffset;
        private void Start()
        {
            _particleOffset = driftParticle.transform.position - transform.position;
            driftParticle.transform.parent = null;
            
            _trailOffset = driftTrail.transform.position - transform.position;
            driftTrail.transform.parent = null;
        }

        public void Play()
        {
            driftParticle.Play();
            driftTrail.emitting = true;
        }

        private void Update()
        {
            driftParticle.transform.position = transform.position + _particleOffset;
            driftTrail.transform.position = transform.position + _trailOffset;
        }

        public void Stop()
        {
            driftParticle.Stop();
            driftTrail.emitting = false;
        }
    }
}