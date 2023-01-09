using System.Collections.Generic;

namespace Lou.Services {
    public interface IPhysicsService {
        bool PointInRectangle(Vector2 x, Vector2 y, Vector2 w, Vector2 z, Vector2 pt);
        bool PointInTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 pt);
        List<Vector3> RotateAroundPoint(List<Vector3> vertices, Vector3 rotation, Vector3 pivot);
    }
}