using System;
using UnityEngine.UIElements;

namespace Code.Core.Extensions
{
    public static class VisualElementExtensions
    {
        public static T GetVisualElement<T>(this VisualElement root, string elementIDName, string goName)
            where T : VisualElement
        {
            var element = root.Q<T>(elementIDName);
            if (element == null) throw new NullReferenceException($"{elementIDName} in {goName} not found");
            return element;
        }
    }
}
