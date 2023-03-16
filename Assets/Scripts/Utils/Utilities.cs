using System;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Utils
{
    public class Utilities
    {
        private const string USS_BUTTON = "button";
        private const string USS_BUTTONFOCUS = "buttonFocus";
        
        private const string USS_BUTTON2 = "button2";
        private const string USS_BUTTON2FOCUS = "button2Focus";
        
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
        
        public static void BindButton(Button button, Action onClick, bool focusable, bool baseStyle)
        {
            button.clicked -= onClick;
            button.clicked += onClick;
                
            if (!focusable) return;
                
            button.RegisterCallback<FocusInEvent>(_ => FocusButton(button, true, baseStyle));
            button.RegisterCallback<FocusOutEvent>(_ => FocusButton(button, false, baseStyle));
        }
        
        private static void FocusButton(Button button, bool focused, bool baseStyle)
        {
            if (focused)
            {
                if (baseStyle)
                {
                    button.RemoveFromClassList(USS_BUTTON);
                    button.AddToClassList(USS_BUTTONFOCUS);
                }
                else
                {
                    button.RemoveFromClassList(USS_BUTTON2);
                    button.AddToClassList(USS_BUTTON2FOCUS);
                }
            }
            else
            {
                if (baseStyle)
                {
                    button.RemoveFromClassList(USS_BUTTONFOCUS);
                    button.AddToClassList(USS_BUTTON);
                }
                else
                {
                    button.RemoveFromClassList(USS_BUTTON2);
                    button.AddToClassList(USS_BUTTON2FOCUS);
                }
            }
        }
    }
}