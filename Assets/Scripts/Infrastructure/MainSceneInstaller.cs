using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;
        [FormerlySerializedAs("_gameManager")] [SerializeField] private MainManager mainManager;
        [SerializeField] private ScenesManager _scenesManager;
        
        public override void InstallBindings()
        {
            Container.Bind<IUIManager>().FromInstance(_uiManager).AsSingle();
            Container.Bind<IMainManager>().FromInstance(mainManager).AsSingle();
            Container.Bind<IScenesManager>().FromInstance(_scenesManager).AsSingle();
        }
    }
}
