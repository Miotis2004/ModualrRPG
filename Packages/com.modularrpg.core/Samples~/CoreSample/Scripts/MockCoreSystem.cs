using ModularRPG.Core;
using UnityEngine;

namespace ModularRPG.Core.Samples
{
    public sealed class MockCoreSystemInstaller : MonoBehaviour
    {
        [SerializeField] private RPGGameManager gameManager;

        private void Awake()
        {
            if (gameManager == null)
            {
                gameManager = FindFirstObjectByType<RPGGameManager>();
            }

            gameManager.RegisterSystem(new MockCoreSystem());
        }
    }

    public sealed class MockCoreSystem : RPGSystemBase, IRPGInitializable, IRPGStartable, IRPGShutdown
    {
        public override string SystemId => "sample.mock-core-system";
        public override int InitializationOrder => 100;

        public void Initialize(IRPGServiceProvider services)
        {
            services.GetService<IRPGDiagnostics>().Log(RPGLogCategory.Initialization, "MockCoreSystem initialized.");
        }

        public void StartSystem(IRPGServiceProvider services)
        {
            services.GetService<IRPGDiagnostics>().Log(RPGLogCategory.Lifecycle, "MockCoreSystem started.");
        }

        public void Shutdown(IRPGServiceProvider services)
        {
            services.GetService<IRPGDiagnostics>().Log(RPGLogCategory.Lifecycle, "MockCoreSystem shut down.");
        }
    }
}
