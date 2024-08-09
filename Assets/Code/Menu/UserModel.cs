using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Rendering.HighDefinition;

namespace Code.Menu
{
    public class UserModel
    {
        private ReactiveProperty<int> _contributionPoint;
        private ReactiveProperty<int> _level;
        private ReactiveProperty<int> _experience;

        public IReadOnlyReactiveProperty<int> ContributionPoint => _contributionPoint;
        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<int> Experience => _experience;

        
        public UserModel(int contributionPoint, int level, int experience)
        {
            _contributionPoint = new ReactiveProperty<int>(contributionPoint);
            _level = new ReactiveProperty<int>(level);
            _experience = new ReactiveProperty<int>(experience);
        }

        public void AddContributionPoint(int point)
        {
            _contributionPoint.Value += point;
            UserRepository.SaveContributionPoint(_contributionPoint.Value);
        }

        public void AddLevel()
        {
            _level.Value++;
            UserRepository.SaveLevel(_level.Value);
        }

        public void AddExperience(int experience)
        {
            _experience.Value += experience;
            UserRepository.SaveExperience(_experience.Value);
        }
    
    }
}

