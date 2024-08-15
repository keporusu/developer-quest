using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Code.Menu
{
    public class UserPresenter : MonoBehaviour
    {
        [SerializeField] private bool _useDummy = false;

        private UserModel _userModel;
    
        void Start()
        {
            if (_useDummy) _userModel = new UserModel(20, 120, 25600);
            else _userModel = new UserModel(UserRepository.LoadContributionPoint(), UserRepository.LoadLevel(),
                UserRepository.LoadExperience());
        
            var uiController = GameObject.Find("UI").GetComponent<UIController>();

            _userModel.ContributionPoint.Subscribe(point =>
            {
                uiController.SetContributionPointGage(point);
            });
        }
    
        public void AddContributionPoint(int point)
        {
            _userModel.AddContributionPoint(point);
        }

        public void AddLevel()
        {
            _userModel.AddLevel();
        }

        public void AddExperience(int experience)
        {
            _userModel.AddExperience(experience);
        }
    }

}
