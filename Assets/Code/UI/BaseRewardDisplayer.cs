using Code.Inventory;
using UnityEngine;

namespace Code.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseRewardDisplayer : MonoBehaviour
    {
        private InventoryItem _inventoryItem;
        private RectTransform _rectTransform;
        public RectTransform RectTransform { get => _rectTransform; }
        public InventoryItem InventoryItem { get => _inventoryItem; }
        public virtual void Construct(InventoryItem inventoryItem)
        {
            _rectTransform = GetComponent<RectTransform>();
            _inventoryItem = inventoryItem;
        }
    }
}