using Presenter;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// ����� ����� ��� ����� ����-����
    /// ��������� ��� ���������� �� �����
    /// </summary>
    public class MetaBootstrap : MonoBehaviour
    {
        private IHeaderPresenter header;
        private ILevelSelectionPresenter levelSelection;

        [Inject]
        public void Construct(IHeaderPresenter header, ILevelSelectionPresenter levelSelection)
        {
            this.header = header;
            this.levelSelection = levelSelection;
        }

        private void Start()
        {
            header.Enable();
            levelSelection.Enable();
        }

        private void OnDestroy()
        {
            header?.Disable();
            levelSelection?.Disable();
        }
    }
}