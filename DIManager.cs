using Zenject;

namespace Lou {
    public static class DIManager {
        public static DiContainer Container { get; private set; }

        public static void Initialize() {
            Container = new DiContainer();
            Container.Install<LouPluginInstaller>();
        }
    }
}