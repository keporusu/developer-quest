using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;


namespace Code.Menu
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject _contributionPopUpPref;
        private GameObject _contributionPopUp;
        
        [SerializeField] private UIView _uiView;
    
        private bool _isPopUpDisabled = false;
        private bool _isFirstGageSetting = true;
        
        private CharacterController _characterController;
    
        
        private void Start()
        {
            _characterController = GameObject.Find("Character").GetComponent<CharacterController>();
    
            _uiView.Battle.Subscribe(async _ =>
            {
                await _characterController.GoBattle();
                SceneTransition.ToBattle();
            }).AddTo(this);
            
            _uiView.Exit.Subscribe(async _ =>
            {
               await _characterController.Exit();
               SceneTransition.EndGame();
            }).AddTo(this);
        }
        
    
        /// <summary>
        /// ポップアップ表示関数
        /// </summary>
        public async void GenerateContributionPopUp(int newContributions, int totalContributions,IList<DayContribution> previous, IList<DayContribution> required)
        {
            var newPopUp = Instantiate(_contributionPopUpPref, transform);
    
            int blankDays;
            var isLastChanged = previous.Any() && required.Any() && (previous.First().Day == required.Last().Day);
            if (isLastChanged) blankDays = 31 - (previous.Count() + required.Count());
            else blankDays = 30 - (previous.Count() + required.Count());
            
            //ポップアップが消えた後に、ゲージが増える
            var contributionPopUpView = newPopUp.GetComponent<ContributionPopUpView>();
            _isPopUpDisabled = await contributionPopUpView.Init(newContributions, totalContributions, blankDays,
                previous.Reverse(), required.Reverse(), isLastChanged);

            _uiView.GitHub.Subscribe(async _ => await contributionPopUpView.Activate()).AddTo(gameObject);
            
        }
    
        public async void SetContributionPointGage(int contributionPoint)
        {
            await UniTask.WaitUntil(() => _isPopUpDisabled ^  _isFirstGageSetting); //最初 or ポップアップが死んでいるなら
            _uiView.SetContributionPointGage(contributionPoint, !_isFirstGageSetting);
            _isFirstGageSetting = false;
        }
    
        public void GoBattle()
        {
            _characterController.GoBattle();
        }
        
        
    }

}
