using log4net.Config;
using NUnit.Framework;

namespace LouPluginTest {
    
    public class LouPluginTest {
        
        [OneTimeSetUp]
        public virtual void Init() {
            XmlConfigurator.Configure(new System.IO.FileInfo($"log4net.conf"));
            TestDIManager.Initialize();
        }
        
    }
}