using NUnit.Framework;
using UnityEngine;

namespace ModularRPG.Core.Editor.Tests
{
    public sealed class CoreEditorTests
    {
        [Test]
        public void GameManagerRegistersCoreServices()
        {
            GameObject host = new GameObject("RPGGameManagerTest");
            try
            {
                RPGGameManager manager = host.AddComponent<RPGGameManager>();
                manager.RegisterCoreServices();
                Assert.IsTrue(manager.TryGetService(out IRPGEventBus eventBus));
                Assert.IsNotNull(eventBus);
                Assert.IsTrue(manager.TryGetService(out IRPGDiagnostics diagnostics));
                Assert.IsNotNull(diagnostics);
            }
            finally
            {
                Object.DestroyImmediate(host);
            }
        }
    }
}
