using Code.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class TooltipWindow : BaseWindow
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private RectTransform _tooltipPanel;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private float _maxWidth = 2300;
        [SerializeField] private float _maxHeigth = 700;

        private float _defaultDescriptionTextSize;
        protected override void OnShow(object[] args)
        {
            InventoryItem inventoryItem = (InventoryItem)args[0];
            InitializeTooltip(inventoryItem);

            if (IsMaxSizeReached())
            {
                RefreshTooltipSize(_maxWidth, _maxHeigth, true);
            }
        }
        protected override void OnHide()
        {
            
        }

        protected override void Awake()
        {
            base.Awake();
            _defaultDescriptionTextSize = _descriptionText.fontSize;
        }

        private void InitializeTooltip(InventoryItem inventoryItem)
        {
            _icon.sprite = inventoryItem.Icon;
            _titleText.text = inventoryItem.Title;
            _descriptionText.text = inventoryItem.Description;
            _descriptionText.fontSize = _defaultDescriptionTextSize;
            
            RefreshTooltipSize(-1, -1);
            Canvas.ForceUpdateCanvases();
        }

        private void RefreshTooltipSize(float width, float height, bool autoSizeState = false)
        {
            _layoutElement.preferredWidth = width;
            _layoutElement.preferredHeight = height;
            SetAutoSizeDescriptionTextState(autoSizeState);
        }
        
        private bool IsMaxSizeReached()
        {
            var currentRect = _tooltipPanel.rect;
            return currentRect.width >= _maxWidth || currentRect.height >= _maxHeigth;
        }

        private void SetAutoSizeDescriptionTextState(bool state)
        {
            _descriptionText.enableAutoSizing = state;
        }
    }
}