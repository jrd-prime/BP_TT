using System;
using System.Collections.Generic;
using Code.Core.Data.Constants;
using Code.Game.Inventory;
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
        [SerializeField] private InventoryMainSettings inventoryMainSettings;
        [SerializeField] private VisualTreeAsset invItemTemplate;

        private IMainUIViewModel _viewModel;

        private VisualElement _root;
        private VisualElement _invContainer;

        private float _invItemContainerWidth;

        private Button _shootBtn;
        private Button _addAmmoBtn;
        private Button _addEquipmentBtn;
        private Button _removeEquipmentBtn;

        private readonly Dictionary<int, VisualElement> _slotsCache = new();
        private bool _isInventoryViewInitialized;

        [Inject]
        private void Construct(IMainUIViewModel viewModel) => _viewModel = viewModel;

        protected override void Init()
        {
            _root = GetComponent<UIDocument>().rootVisualElement ?? throw new NullReferenceException("Root is null.");

            if (!invItemTemplate) throw new NullReferenceException("Inventory item template not set!");

            if (_viewModel == null) throw new NullReferenceException("Main UI ViewModel is null");

            _viewModel.InventoryData.Skip(1).Subscribe(OnInventoryDataChanged).AddTo(Disposables);
        }

        private void OnInventoryDataChanged(InventoryData data)
        {
            Debug.LogWarning("on inventory data changed");
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

            _invItemContainerWidth = invContainerWidth / inventoryMainSettings.ItemsInRow;

            InitializeInventoryView();
        }

        private void InitializeInventoryView()
        {
            //locked slots?
            Debug.LogWarning("initialize inventory view");
            for (var i = 0; i < inventoryMainSettings.MaxSlots; i++)
            {
                var template = invItemTemplate.Instantiate();
                var itemContainer = template.Q<VisualElement>(UINameID.InvItemContainer) ??
                                    throw new NullReferenceException("Inventory item container not found in template!");

                itemContainer.style.width = _invItemContainerWidth;
                itemContainer.style.height = _invItemContainerWidth;

                _invContainer.Add(template);
                _slotsCache.Add(i, itemContainer);
            }

            _isInventoryViewInitialized = true;
        }
    }
}
