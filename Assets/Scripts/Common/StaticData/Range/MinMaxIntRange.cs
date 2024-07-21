using System;
using Random = UnityEngine.Random;

namespace Common.StaticData.Range
{
    [Serializable]
    public sealed class MinMaxIntRange : MinMaxRange<int>
    {
        public override void OnValidate()
        {
            if (Min > Max)
            {
                Min = Max;
            }
        }

        public override int GetRandom()
        {
            return Random.Range(Min, Max + 1);
        }
    }
}