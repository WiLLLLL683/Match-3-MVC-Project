using Config;
using Model.Objects;
using Model.Services;
using Presenter;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// ����������� �� ����� ����-���� ����������� � GameStateMachine
    /// </summary>
    public class MetaDependencies : MonoBehaviour
    {
        public IHeaderPresenter header;
        public ILevelSelectionPresenter levelSelection;

        [Inject]
        public void Construct(IHeaderPresenter header, ILevelSelectionPresenter levelSelection)
        {
            this.header = header;
            this.levelSelection = levelSelection;
        }
    }
}