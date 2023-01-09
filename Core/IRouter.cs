using System;
using Lou.Controllers;

namespace Lou {
    public interface IRouter {
        void Initialize();
        /**
         * Registers a controller's routes.
         */
        void RegisterController(IController controller);
        /**
         * Attempts to find a handler for async messages from Lua.
         */
        Action<LouRequest> RouteRequest(LouRequest request);
        /**
         * Attempts to find a handler for blocking requests from Lua.
         */
        Func<LouDirectRequest, object[]> RouteDirectRequest(LouDirectRequest directRequest);
    }
}