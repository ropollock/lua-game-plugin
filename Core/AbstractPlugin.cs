using System.Collections.Concurrent;
using Gameplay.Plugin;

namespace Lou {
    public abstract class AbstractPlugin : IPlugin {
        
        public abstract void MessageFromLua(LuaRequest request);
        public abstract object[] DirectRequestFromLua(object[] _requestData);
        public abstract string GetPluginName();

        protected bool isReadyForStartup = false;
        protected bool isReadyForShutdown = false;

        /**
         * Initialize this plugin.
         *
         * This is called by the server after the simulation starts, but before users are accepted.
         * param _responseQueue The queue that Lua responses should be pushed to.
         * param _clusterId The cluster id for this server (blank if running in standalone mode).
         * param _regionAddress The region address of this particular server.
         */
        public virtual void Init(ConcurrentQueue<LuaResponse> _responseQueue, string _clusterId,
            string _regionAddress) {
            isReadyForStartup = true;
        }

        /**
         * Return whether or not this plugin has finished initializing.
         * This is called by the server after `Init` has been called to determine if the plugin can `Start` yet.
         */
        public virtual bool IsReadyForServerStartup() {
            return isReadyForStartup;
        }

        /**
         * This is called by the server after the plugin has signaled that it is ready for startup.
         */
        public virtual void Start() { }
        
        public virtual void Shutdown() {
            isReadyForShutdown = true;
        }

        public virtual bool IsShutDown() {
            return isReadyForShutdown;
        }

        #region Unimplemented interface members
        public virtual void WriteString(string _value) {
            throw new System.NotImplementedException();
        }

        public virtual void WriteDouble(double _value) {
            throw new System.NotImplementedException();
        }

        public virtual void WriteBoolean(bool _value) {
            throw new System.NotImplementedException();
        }

        public virtual void WriteInt32(int _value) {
            throw new System.NotImplementedException();
        }

        public virtual void WriteInt64(long _value) {
            throw new System.NotImplementedException();
        }

        public virtual void WriteObject(object _value) {
            throw new System.NotImplementedException();
        }

        public virtual string ReadString() {
            throw new System.NotImplementedException();
        }

        public virtual double ReadDouble() {
            throw new System.NotImplementedException();
        }

        public virtual bool ReadBoolean() {
            throw new System.NotImplementedException();
        }

        public virtual int ReadInt32() {
            throw new System.NotImplementedException();
        }

        public virtual long ReadInt64() {
            throw new System.NotImplementedException();
        }

        public virtual object ReadObject() {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}