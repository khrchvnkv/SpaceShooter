using System;
using Random = UnityEngine.Random;

namespace Common.StaticData.Range
{
    [Serializable]
    public sealed class MinMaxUintRange : MinMaxRange<uint>
    {
        public override void OnValidate()
        {
            if (Min > Max)
            {
                Min = Max;
            }
        }

        public override uint GetRandom()
        {
            return (uint)Random.Range(Min, Max + 1);
        }
    }
}