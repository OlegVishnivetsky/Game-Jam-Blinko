using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level Info", fileName = "Level Info")]
    public class LevelInfo : ScriptableObject
    {
        [Title("Falling Char Spawn Parameters")]
        [SerializeField] private float _minSpawnTime;
        [SerializeField] private float _maxSpawnTime;

        public float MinSpawnTime => _minSpawnTime;
        public float MaxSpawnTime => _maxSpawnTime;
    }
}