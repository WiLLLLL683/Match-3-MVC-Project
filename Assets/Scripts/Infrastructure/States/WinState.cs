using Config;
using Cysharp.Threading.Tasks;
using Model.Services;
using System.Collections;
using System.Threading;
using Utils;

namespace Infrastructure
{
    public class WinState : IState
    {
        private readonly IWinLoseService winLoseService;
        private readonly IConfigProvider configProvider;

        public WinState(IWinLoseService winLoseService, IConfigProvider configProvider)
        {
            this.winLoseService = winLoseService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            await UniTask.WaitForSeconds(configProvider.Delays.beforeWin, cancellationToken: token);
            winLoseService.RaiseWinEvent();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}