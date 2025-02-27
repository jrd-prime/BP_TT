using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UI
{
    public abstract class UIViewBase : MonoBehaviour, IDisposable
    {
        protected readonly Dictionary<Button, EventCallback<ClickEvent>> CallbacksCache = new();
        protected readonly CompositeDisposable Disposables = new();

        private void Start()
        {
            Init();
            InitElements();
            InitCallbacksCache();
            RegisterCallbacks();
        }

        protected abstract void Init();
        protected abstract void InitElements();
        protected abstract void InitCallbacksCache();

        private void RegisterCallbacks()
        {
            foreach (var (button, callback) in CallbacksCache) button.RegisterCallback(callback);
        }

        private void UnregisterCallbacks()
        {
            foreach (var (button, callback) in CallbacksCache) button.UnregisterCallback(callback);
        }

        protected static void CheckVisualElementOnNull(VisualElement element, string elementIDName, string className)
        {
            if (element == null) throw new NullReferenceException($"{elementIDName} in {className} is null");
        }

        public void Dispose() => Unregister();


        private void Unregister()
        {
            Debug.LogWarning("unregister view callback " + name);
            UnregisterCallbacks();
        }
    }
}
