using System;
using System.Collections.Generic;
using Code.Core.Data.Constants;
using Code.Core.Data.SO;
using Code.Core.Tools;
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

        private float _invItemContainerWidth;

        private Button _shootBtn;
        private Button _addAmmoBtn;
        private Button _addEquipmentBtn;
        private Button _removeEquipmentBtn;

        private readonly Dictionary<int, VisualElement> _slotsCache = new();
        private bool _isInventoryViewInitialized;
        private VisualElement _inventoryContainer;
        private VisualElement _unityContentContainer;


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
            _shootBtn = _root.GetVisualElement<Button>(UINameID.ShootBtn, name);
            _addAmmoBtn = _root.GetVisualElement<Button>(UINameID.AddAmmoBtn, name);
            _addEquipmentBtn = _root.GetVisualElement<Button>(UINameID.AddEquipmentBtn, name);
            _removeEquipmentBtn = _root.GetVisualElement<Button>(UINameID.RemoveEquipmentBtn, name);

            _inventoryContainer = _root.GetVisualElement<VisualElement>(UINameID.InventoryContainer, name);

            _inventoryContainer.RegisterCallback<GeometryChangedEvent>(OnInvContainerGeometryChanged);
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
            _inventoryContainer.UnregisterCallback<GeometryChangedEvent>(OnInvContainerGeometryChanged);

            var inventoryContainerWidth = _inventoryContainer.resolvedStyle.width;

            if (float.IsNaN(inventoryContainerWidth)) throw new Exception("Inventory container width is NaN!");

            ConfigureScrollView(inventoryContainerWidth);
            _invItemContainerWidth = inventoryContainerWidth / inventoryMainSettings.ItemsInRow;

            InitializeInventoryView();
        }

        private void ConfigureScrollView(float inventoryContainerWidth)
        {
            var scrollView = _root.GetVisualElement<ScrollView>(UINameID.InventoryScrollView, name);
            scrollView.style.width = inventoryContainerWidth;

            _unityContentContainer = _root.GetVisualElement<VisualElement>(UINameID.UnityContentContainer, name);
            _unityContentContainer.style.flexDirection = FlexDirection.Row;
            _unityContentContainer.style.flexWrap = Wrap.Wrap;
        }

        private void InitializeInventoryView()
        {
            //locked slots?
            Debug.LogWarning("initialize inventory view");
            for (var i = 0; i < inventoryMainSettings.MaxSlots; i++)
            {
                var template = invItemTemplate.Instantiate();
                var itemContainer = template.GetVisualElement<VisualElement>(UINameID.InvItemContainer, name);

                itemContainer.style.width = _invItemContainerWidth;
                itemContainer.style.height = _invItemContainerWidth;

                var slotId = itemContainer.GetVisualElement<Label>(UINameID.InventorySlotIdLabel, name);
                slotId.text = (i + 1).ToString();

                _unityContentContainer.Add(template);
                _slotsCache.Add(i, itemContainer);
            }

            _isInventoryViewInitialized = true;
        }
    }
}
