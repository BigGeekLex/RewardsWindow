using System.Collections;
using Code.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI
{
    public class RewardDisplayerWithTooltip : BaseRewardDisplayer, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]
        private Image _icon;
        
        private const float TapDelay = 0.5f;
        public override void Construct(InventoryItem inventoryItem)
        {
            base.Construct(inventoryItem);
            
            if(_icon == null) Debug.LogError($"Null reward icon reference for: {gameObject.name}");
            SetRewardIcon(inventoryItem.Icon);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAllCoroutines();
            BaseWindow.Get<TooltipWindow>().Hide();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(ShowTooltipRoutine());
        }

        private IEnumerator ShowTooltipRoutine()
        {
            yield return new WaitForSecondsRealtime(TapDelay);
            BaseWindow.Get<TooltipWindow>().Show(InventoryItem);   
        }
        private void SetRewardIcon(Sprite rewardIcon)
        {
            if(_icon != null) _icon.sprite = rewardIcon;
        }
    }
}