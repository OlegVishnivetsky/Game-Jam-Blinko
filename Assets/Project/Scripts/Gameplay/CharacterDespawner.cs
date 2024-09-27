using System.Collections;
using UnityEngine;
using R3;
using R3.Triggers;
using Lean.Pool;

namespace Gameplay
{
    public class CharacterDespawner : MonoBehaviour
    {
        [SerializeField] Collider2D _collider;
        [SerializeField] ParticleSystem _hit1PRefab;
        [SerializeField] ParticleSystem _hit2PRefab;

        private void Awake()
        {
            _collider.OnCollisionEnter2DAsObservable().Subscribe(OnColliderCharacter).AddTo(this);


        }

        private void OnColliderCharacter(Collision2D collision)
        {
            var fx = LeanPool.Spawn(_hit1PRefab);
            fx.transform.position = collision.transform.position;
            fx.Play();
            fx = LeanPool.Spawn(_hit2PRefab);
            fx.transform.position = collision.transform.position;
            fx.Play();

            collision.transform.Disactivate();
        }
    }
}