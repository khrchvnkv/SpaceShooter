using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.StaticData.Range
{
    [Serializable]
    public sealed class MinMaxFloatRange : MinMaxRange<float>
    {
        [SerializeField] private bool _canBeNegative;

        public override void OnValidate()
        {
            if (!_canBeNegative)
            {
                Min = Mathf.Clamp(Min, 0.0f, float.MaxValue);
                Max = Mathf.Clamp(Max, 0.0f, float.MaxValue);
            }
            
            if (Min > Max)
            {
                Min = Max;
            }
        }

        public override float GetRandom()
        {
            return Random.Range(Min, Max);
        }
    }
}