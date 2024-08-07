using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UserPresneter : MonoBehaviour
{

    private UserModel _userModel;
    
    void Start()
    {
        _userModel = new UserModel(UserRepository.LoadContributionPoint(), UserRepository.LoadLevel(),
            UserRepository.LoadExperience());
        
        var uiController = GameObject.Find("UI").GetComponent<UIController>();

        _userModel.ContributionPoint.Subscribe(_ =>
        {
            //uiController.
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
