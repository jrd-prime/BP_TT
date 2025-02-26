using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UI
{
    //TODO safe area for mobile
    [RequireComponent(typeof(UIDocument))]
    public sealed class MainUIView : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset invItemTemplate;
        [SerializeField] private int itemsCount = 15;
        [SerializeField] private int invItemsInRow = 5;

        private VisualElement _root;
        private VisualElement invContainer;

        private float invContainerWidth;
        private float invItemContainerWidth;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement ?? throw new NullReferenceException("Root is null.");
            if (!invItemTemplate) throw new NullReferenceException("Inventory item template not set!");

            invContainer = _root.Q<VisualElement>("inv-container");

            invContainer.RegisterCallback<GeometryChangedEvent>(onInvContainerGeometryChanged);
        }

        private void onInvContainerGeometryChanged(GeometryChangedEvent evt)
        {
            invContainer.UnregisterCallback<GeometryChangedEvent>(onInvContainerGeometryChanged);

            invContainerWidth = invContainer.resolvedStyle.width;

            invItemContainerWidth = invContainerWidth / invItemsInRow;

            Debug.Log($"invContainerWidth: {invContainerWidth}, invItemContainerWidth: {invItemContainerWidth}");

            FillInventoryView();
        }

        private void FillInventoryView()
        {
            for (int i = 0; i < itemsCount; i++)
            {
                var template = invItemTemplate.Instantiate();
                var itemContainer = template.Q<VisualElement>("inv-item-container");

                itemContainer.style.width = invItemContainerWidth;
                itemContainer.style.height = invItemContainerWidth;
                invContainer.Add(template);
            }
        }
    }
}
