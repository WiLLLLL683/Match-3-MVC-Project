using Model.Factories;
using Model.Objects;
using Model.Services;
using UnityEngine;
using Utils;
using Zenject;

namespace CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindModel();
            BindFactories();
            BindServices();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesTo<StateMachine>().AsSingle();
        }

        private void BindModel()
        {
            Container.Bind<Game>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IBlockFactory>().To<BlockFactory>().AsSingle();
            Container.Bind<ICellFactory>().To<CellFactory>().AsSingle();
            Container.Bind<IGameBoardFactory>().To<GameBoardFactory>().AsSingle();
            Container.Bind<IMatchPatternFactory>().To<MatchPatternFactory>().AsSingle();
            Container.Bind<IHintPatternFactory>().To<HintPatternFactory>().AsSingle();
            Container.Bind<ICounterFactory>().To<CounterFactory>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            Container.Bind<IBoosterFactory>().To<BoosterFactory>().AsSingle();
        }

        private void BindServices()
        {
            //block services
            Container.Bind<IBlockChangeTypeService>().To<BlockChangeTypeService>().AsSingle();
            Container.Bind<IBlockDestroyService>().To<BlockDestroyService>().AsSingle();
            Container.Bind<IBlockMoveService>().To<BlockMoveService>().AsSingle();
            Container.Bind<IBlockSpawnService>().To<BlockSpawnService>().AsSingle();
            Container.Bind<IMatcher>().To<Matcher>().AsSingle();
            Container.Bind<IBlockMatchService>().To<BlockMatchService>().AsSingle();
            Container.Bind<IBlockGravityService>().To<BlockGravityService>().AsSingle();
            Container.Bind<IBlockRandomTypeService>().To<BlockRandomTypeService>().AsSingle();
            Container.Bind<IValidationService>().To<ValidationService>().AsSingle();

            //cell Services
            Container.Bind<ICellChangeTypeService>().To<CellChangeTypeService>().AsSingle();
            Container.Bind<ICellSetBlockService>().To<CellSetBlockService>().AsSingle();
            Container.Bind<ICellDestroyService>().To<CellDestroyService>().AsSingle();

            //other
            Container.Bind<IBoosterService>().To<BoosterService>().AsSingle();
            Container.Bind<ICounterService>().To<CounterService>().AsSingle();
            Container.Bind<ICurrencyService>().To<CurrencyService>().AsSingle();
            Container.Bind<IWinLoseService>().To<WinLoseService>().AsSingle();
        }
    }
}