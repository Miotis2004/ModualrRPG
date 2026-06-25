using System.Collections.Generic;
using NUnit.Framework;

namespace ModularRPG.Core.Tests
{
    public sealed class CoreRuntimeTests
    {
        [Test]
        public void RegistryRejectsDuplicateServiceRegistration()
        {
            RPGSystemRegistry registry = new RPGSystemRegistry();
            object service = new object();
            registry.RegisterService(service);
            Assert.Throws<System.InvalidOperationException>(() => registry.RegisterService(service));
        }

        [Test]
        public void RegistryReturnsSystemsInDeterministicOrder()
        {
            RPGSystemRegistry registry = new RPGSystemRegistry();
            OrderedSystem late = new OrderedSystem("late", 20);
            OrderedSystem early = new OrderedSystem("early", 1);
            registry.RegisterSystem(late);
            registry.RegisterSystem(early);

            IReadOnlyList<IRPGSystem> systems = registry.GetSystemsInInitializationOrder();
            Assert.AreEqual("early", systems[0].SystemId);
            Assert.AreEqual("late", systems[1].SystemId);
        }

        [Test]
        public void EventBusDoesNotInvokeUnsubscribedListener()
        {
            RPGEventBus bus = new RPGEventBus();
            int calls = 0;
            System.Action<TestEvent> listener = _ => calls++;
            bus.Subscribe(listener);
            bus.Publish(new TestEvent());
            bus.Unsubscribe(listener);
            bus.Publish(new TestEvent());
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void IdentifierCreatesStableValue()
        {
            RPGIdentifier identifier = RPGIdentifier.NewIdentifier();
            RPGIdentifier copy = new RPGIdentifier(identifier.Value);
            Assert.IsTrue(identifier.IsValid);
            Assert.AreEqual(identifier, copy);
        }

        [Test]
        public void ValidatorReportsDuplicateSystemIds()
        {
            RPGSystemRegistry registry = new RPGSystemRegistry();
            registry.RegisterSystem<IRPGSystem>(new OrderedSystem("duplicate", 1));
            registry.RegisterSystem(new DuplicateContractSystem("duplicate", 2));

            RPGValidationReport report = RPGCoreValidator.ValidateRegistry(registry);
            Assert.IsFalse(report.IsValid);
        }

        private sealed class TestEvent : IRPGEvent { }

        private sealed class OrderedSystem : RPGSystemBase
        {
            private readonly string id;
            private readonly int order;

            public OrderedSystem(string id, int order)
            {
                this.id = id;
                this.order = order;
            }

            public override string SystemId => id;
            public override int InitializationOrder => order;
        }

        private sealed class DuplicateContractSystem : RPGSystemBase
        {
            private readonly string id;
            private readonly int order;

            public DuplicateContractSystem(string id, int order)
            {
                this.id = id;
                this.order = order;
            }

            public override string SystemId => id;
            public override int InitializationOrder => order;
        }
    }
}
