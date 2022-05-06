using Managers;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ScenesManager _scenesManager;
        
        public override void InstallBindings()
        {
            Container.Bind<IUIManager>().FromInstance(_uiManager).AsSingle();
            Container.Bind<IScenesManager>().FromInstance(_scenesManager).AsSingle();
        }
    }
}