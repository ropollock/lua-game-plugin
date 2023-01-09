
namespace Lou.Controllers {
    public interface IController {
        void Initialize();
        void Start();
        void Shutdown();
        /**
         * Provide a Routes definition for router registration.
         */
        Routes GetRoutes();
    }
}