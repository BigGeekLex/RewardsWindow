using System.Collections.Generic;
using Code.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class RewardsWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private BaseRewardDisplayer baseRewardDisplayerPrefab;
        [SerializeField] private RectTransform _rewardsParent;
        
        private Vector2 _cetricPivot = new Vector2(0.5f, 0.5f);
        private Vector2 _defaultPivot;
        protected override void Awake()
        {
            base.Awake();
            _closeButton.onClick.AddListener(Hide);
            _defaultPivot = _rewardsParent.pivot;
        }

        protected override void OnShow(object[] args)
        {
            _titleText.text = (string)args[0];

            var items = (List<InventoryItem>)args[1];

            InitializeRewards(items);
        }
        protected override void OnHide()
        {
            ClearRewards();
        }
        
        private void InitializeRewards(List<InventoryItem> rewards)
        {
            float totalWidth = 0;
            
            foreach (var reward in rewards)
            {
                var rewardDisplayer = Instantiate(baseRewardDisplayerPrefab, Vector3.zero, Quaternion.identity, _rewardsParent);
                rewardDisplayer.Construct(reward);
                
                float currentRewardWidth = rewardDisplayer.RectTransform.rect.width;
                totalWidth += currentRewardWidth;
            }
            ChangePivotPosition(totalWidth);
        }

        private void ChangePivotPosition(float totalWidth)
        {
            if (IsScreenOut(totalWidth)){ ChangePivot(_defaultPivot); return;}
        
            ChangePivot(_cetricPivot);
        }
        
        private bool IsScreenOut(float targetWidth)
        {
            if (Screen.width <= targetWidth) return true;
        
            return false;
        }

        private void ChangePivot(Vector2 targetPivot)
        {
            _rewardsParent.pivot = targetPivot;
        }
        
        private void ClearRewards()
        {
            foreach (var spawnedReward in _rewardsParent.GetComponentsInChildren<BaseRewardDisplayer>())
            {
                Destroy(spawnedReward.gameObject);
            }
        }
    }
}