using Model.Infrastructure;
using Model.Readonly;
using System;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public interface IEndGamePresenter : IPresenter
    {
        public void ShowCompletePopUp();
        public void ShowDefeatPopUp();
    }
}