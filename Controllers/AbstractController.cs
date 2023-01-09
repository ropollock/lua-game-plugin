namespace Lou.Controllers {
    public abstract class AbstractController : IController {
        public abstract Routes GetRoutes();

        public virtual void Initialize() { }

        public virtual void Start() { }

        public virtual void Shutdown() { }
    }
}