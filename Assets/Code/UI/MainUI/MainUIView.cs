using System;
using Code.Core.Data.Constants;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Code.UI.MainUI
{
    //TODO safe area for mobile
    [RequireComponent(typeof(UIDocument))]
    public sealed class MainUIView : UIViewBase
    {
        [SerializeField] private VisualTreeAsset invItemTemplate;
        [SerializeField] private int itemsCount = 15;
        [SerializeField] private int invItemsInRow = 5;

        private IMainUIViewModel _viewModel;

        private VisualElement _root;
        private VisualElement _invContainer;

        private float _invItemContainerWidth;

        private Button _shootBtn;
        private Button _addAmmoBtn;
        private Button _addEquipmentBtn;
        private Button _removeEquipmentBtn;

        [Inject]
        private void Construct(IMainUIViewModel viewModel) => _viewModel = viewModel;

        protected override void Init()
        {
            _root = GetComponent<UIDocument>().rootVisualElement ?? throw new NullReferenceException("Root is null.");
            if (!invItemTemplate) throw new NullReferenceException("Inventory item template not set!");
        }

        protected override void InitElements()
        {
            _shootBtn = _root.Q<Button>(UINameID.ShootBtn);
            _addAmmoBtn = _root.Q<Button>(UINameID.AddAmmoBtn);
            _addEquipmentBtn = _root.Q<Button>(UINameID.AddEquipmentBtn);
            _removeEquipmentBtn = _root.Q<Button>(UINameID.RemoveEquipmentBtn);

            CheckOnNull(_shootBtn, UINameID.ShootBtn, name);
            CheckOnNull(_addAmmoBtn, UINameID.AddAmmoBtn, name);
            CheckOnNull(_addEquipmentBtn, UINameID.AddEquipmentBtn, name);
            CheckOnNull(_removeEquipmentBtn, UINameID.RemoveEquipmentBtn, name);

            _invContainer = _root.Q<VisualElement>(UINameID.InventoryContainer);
            CheckOnNull(_invContainer, UINameID.InventoryContainer, name);

            _invContainer.RegisterCallback<GeometryChangedEvent>(OnInvContainerGeometryChanged);
        }

        protected override void InitCallbacksCache()
        {
            if (_viewModel == null) throw new NullReferenceException("Main UI ViewModel is null");

            CallbacksCache.Add(_shootBtn, _ => _viewModel.ShootBtnClick.OnNext(Unit.Default));
            CallbacksCache.Add(_addAmmoBtn, _ => _viewModel.AddAmmoBtnClick.OnNext(Unit.Default));
            CallbacksCache.Add(_addEquipmentBtn, _ => _viewModel.AddEquipmentBtnClick.OnNext(Unit.Default));
            CallbacksCache.Add(_removeEquipmentBtn, _ => _viewModel.RemoveEquipmentBtnClick.OnNext(Unit.Default));
        }


        private void OnInvContainerGeometryChanged(GeometryChangedEvent evt)
        {
            _invContainer.UnregisterCallback<GeometryChangedEvent>(OnInvContainerGeometryChanged);

            var invContainerWidth = _invContainer.resolvedStyle.width;

            if (float.IsNaN(invContainerWidth)) throw new Exception("Inventory container width is NaN!");

            _invItemContainerWidth = invContainerWidth / invItemsInRow;

            FillInventoryView();
        }

        private void FillInventoryView()
        {
            for (int i = 0; i < itemsCount; i++)
            {
                var template = invItemTemplate.Instantiate();
                var itemContainer = template.Q<VisualElement>(UINameID.InvItemContainer) ??
                                    throw new NullReferenceException("Inventory item container not found in template!");

                itemContainer.style.width = _invItemContainerWidth;
                itemContainer.style.height = _invItemContainerWidth;
                _invContainer.Add(template);
            }
        }
    }
}
