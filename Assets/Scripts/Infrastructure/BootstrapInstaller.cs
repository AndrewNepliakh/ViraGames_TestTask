using Managers;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStateManager>().To<StateManager>().AsSingle();
            Container.Bind<IUserManager>().To<UserManager>().AsSingle();
        }
    }
    
}
