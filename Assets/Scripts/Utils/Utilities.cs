using UnityEngine;

namespace Utils
{
    public class Utilities
    {
        public static float RandomRangeWithExclusion(float min, float max, float minExclusion, float maxExclusion, int abortCount = 10000)
        {
            int count = 0;
            float value = Random.Range(min, max);
            while (value >= minExclusion && value <= maxExclusion && count < abortCount)
            {
                value = Random.Range(min, max);
                count++;
            }
            return value;
        }
    }
}