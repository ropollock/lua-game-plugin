using System;
using Zenject;

namespace LouPluginTest {
    public static class TestDIManager {
        public static DiContainer Container { get; private set; }

        public static void Initialize() {
            Container = new DiContainer();
            Container.Install<TestInstaller>();
        }
    }
}