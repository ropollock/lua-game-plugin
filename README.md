# Game Plugin
A C# plugin that implements the LOA plugin interface allowing bidirectional communication between Lua server context and a C# runtime.
Provides an RPC model controller and router scheme to expose various commands. 

## Build
Requires references to LOA libraries:
```
\build\NLua.dll
\build\PluginInterface.dll
\build\ShardsServer.exe
```
Requires Zenject standalone libraries for dependency injection.
Requires Discord.Net nuget package.

## Install
Add `LouPlugin.dll` and project dependencies to `\build\base\plugins` of the server build.

## Usage
For plugin interface reference the LOA admin wiki server plugins page.

Synchronous requests:
```lua
SendRequestToPlugin("LouPlugin", "CommandID", "arg1", "arg2")
```

Asynchronous requests:
```lua
SendMessageToPlugin("LouPlugin", "CommandID", "ResponseTargetID", "HandlerID", "args")
```

### Logging Levels
Verbosity of logging can be configured using a function like this.
`logging.logLevel` property corresponds to integer values that are increasingly strict.
```lua
SetPluginLogLevel = function(level)
  -- 6 None
  -- 5 Error
  -- 4 Warning
  -- 3 Info
  -- 2 Debug
  -- 1 All
    local config = {
        logLevel = level
    }
    SendRequestToPlugin("LouPlugin", "Config.Set", "logging", config)
end
```