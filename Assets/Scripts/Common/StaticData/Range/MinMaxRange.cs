using UnityEngine;

namespace Common.StaticData.Range
{
    public abstract class MinMaxRange<T> where T : struct
    {
        [SerializeField] protected T Min;
        [SerializeField] protected T Max;

        public abstract void OnValidate();
        public abstract T GetRandom();
    }
}