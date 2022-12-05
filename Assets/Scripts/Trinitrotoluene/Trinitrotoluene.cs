using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Trinitrotoluene
{
    public static class LogUtils
    {
        public static string Say(GameObject gameObject, string message)
        {
            return gameObject.name + " says: " + message;
        }
    }
    public static class Utils
    {
    }
    public static class RectTransformTools
    {
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
    }
    public static class Math
    {
        /// <summary>
        /// Checks if an int is between the min and max values.
        /// </summary>
        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value < max;
        }
        /// <summary>
        /// Checks if a float is between the min and max values.
        /// </summary>
        public static bool IsBetween(this float value, float min, float max)
        {
            return value > min && value < max;
        }
        public static bool IsBetweenInclusive(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
        public static class f
        {
            /// <summary>
            /// Returns a normally rounded float but if it has .5 at the end, instead of rounding to the even number, it rounds down.
            /// </summary>
            public static float RoundLow(float value)
            {
                return value % 1 == 0.5f ? value - 0.5f : Mathf.Round(value);
            }
            /// <summary>
            /// Returns a normally rounded integer but if it has .5 at the end, instead of rounding to the even number, it rounds down.
            /// </summary>
            public static int RoundLowToInt(float value)
            {
                return value % 1 == 0.5f ? Mathf.FloorToInt(value - 0.5f) : Mathf.RoundToInt(value);
            }
            public static float RoundTenth(float value)
            {
                return Mathf.Round(value * 10f) / 10f;
            }
            /// <summary>
            /// Returns a rounded float but instead of rounding to the nearest whole number it rounds to the nearest tenth.
            /// </summary>
            public static string RoundTenthToString(float value)
            {
                return (Mathf.Round(value * 10f) / 10f).ToString();
            }

            /// <summary>
            /// Returns a random float between min and max.
            /// </summary>

            public static float Random(float min, float max)
            {
                return UnityEngine.Random.Range(min, max);
            }
        }
        public static class i
        {
            /// <summary>
            /// Returns a random integer between min and max.
            /// </summary>
            public static int Random(int min, int max)
            {
                return UnityEngine.Random.Range(min, max);
            }
            public static int RandomInclusive(int min, int max)
            {
                return UnityEngine.Random.Range(min, max + 1);
            }
        }
        public static class v2
        {
            /// <summary>
            /// Returns a random number for each value in the vector between the minimum and maximum for that value.
            /// </summary>
            public static Vector2 Random(Vector2 min, Vector2 max)
            {
                return new Vector2(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y));
            }
        }
        public static class v3
        {
            /// <summary>
            /// Returns a random number for each value in the vector between the minimum and maximum for that value.
            /// </summary>
            public static Vector3 Random(Vector3 min, Vector3 max)
            {
                return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
            }
        }
        public static class v4
        {
            /// <summary>
            /// Returns a random number for each value in the vector between the minimum and maximum for that value.
            /// </summary>
            public static Vector4 Random(Vector4 min, Vector4 max)
            {
                return new Vector4(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z), UnityEngine.Random.Range(min.w, max.w));
            }
        }
    }
    public static class Functions
    {
        /// <summary>
        /// Returns a scale to multiply to  width and height, to increase them by the same amount.
        /// </summary>
        public static Vector2 scaleEqually(Vector2 size, float increase)
        {
            return new Vector2((size.x + increase) / size.x, (size.y + increase) / size.y);
        }
        /// <summary>
        /// Moves value toward <end>, will be near it in <time> seconds.
        /// </summary>
        public static void MoveTowards(ref this float current, float end, float time)
        {
            current += (end - current) * Mathf.Clamp01(Time.deltaTime / time);
        }
        /// <summary>
        /// Returns true about every second at 100%, every 5 seconds at 20%, and 1/5 of a second at 500%.
        /// </summary>
        public static bool ChancePerSec(float percentage)
        {
            return UnityEngine.Random.Range(0f, 100f) < Mathf.Clamp(percentage * Time.deltaTime, 0, 100);
        }
        public static bool Chance(float percentage)
        {
            return UnityEngine.Random.Range(0f, 100f) < Mathf.Clamp(percentage, 0, 100);
        }
        public static float PercentToMult(this float percent)
        {
            return 1 + (percent / 100);
        }
        public static float PercentToMult(this int percent)
        {
            return 1 + (percent / 100);
        }
        /// <summary>
        /// Used for transitions.
        /// </summary>

        public static void LoadScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }
        /// <summary>
        /// Used for transitions.
        /// </summary>
        public static void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
        public static class f
        {
            /// <summary>
            /// Returns an increment toward the end from current based on the time.
            /// </summary>
            public static float MoveTowards(float current, float end, float time)
            {
                return (end - current) * Mathf.Clamp01(Time.deltaTime / time);
            }
        }
        public static class v2
        {
            /// <summary>
            /// Returns an increment toward the end from current based on the time for all values.
            /// </summary>
            public static Vector2 MoveTowards(Vector2 current, Vector2 end, float time)
            {
                return (end - current) * Mathf.Clamp01(Time.deltaTime / time);
            }
        }
        public static class v4
        {
            /// <summary>
            /// Returns an increment toward the end from current based on the time for all values.
            /// </summary>
            public static Vector4 MoveTowards(Vector4 current, Vector4 end, float time)
            {
                return (end - current) * Mathf.Clamp01(Time.deltaTime / time);
            }
        }
    }
    public static class ConstantData
    {
        public static int[] XPthreshold = { 0, 3, 9, 16, 27, 37, 50, 70, 95, 130, 170, 220, 280, 350, 430, 520, 620, 730, 850, 1000, 1150, 1300, 1500, 1700, 2000, 2300, 2600, 3000, 3400, 4000, 4600, 5200, 6000, 6800, 7800, 8800, 10000, 12000, 14000, 16000, 18000, 20000, 22000, 24000, 26000, 28000, 30000, 32000, 34000, 36000, 38000, 40000, 42000, 44000, 46000, 48000, 50000, 52000, 54000, 56000, 58000, 60000, 63000, 66000, 69000, 72000, 75000, 78000, 82000, 86000, 90000, 95000, 100000, 105000, 110000, 115000, 120000, 125000, 130000, 135000, 140000, 145000, 150000, 155000, 160000, 165000, 170000, 180000, 190000, 200000, 210000, 220000, 230000, 240000, 250000, 260000, 270000, 280000, 290000, 300000, 310000 };
    }
    public static class Data
    {

    }
    public class WhyDoesThisHappenException : Exception
    {
        public WhyDoesThisHappenException()
        {
        }

        public WhyDoesThisHappenException(string message)
            : base(message)
        {
        }

        public WhyDoesThisHappenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
