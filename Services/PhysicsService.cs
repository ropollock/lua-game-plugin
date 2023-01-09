using System.Collections.Generic;

namespace Lou.Services {
    public class PhysicsService : IPhysicsService {
        public bool PointInTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 pt) {
            // Compute vectors
            Vector2 v0 = c - a;
            Vector2 v1 = b - a;
            Vector2 v2 = pt - a;

            // Compute dot products
            float dot00 = Vector2.Dot(v0, v0);
            float dot01 = Vector2.Dot(v0, v1);
            float dot02 = Vector2.Dot(v0, v2);
            float dot11 = Vector2.Dot(v1, v1);
            float dot12 = Vector2.Dot(v1, v2);

            // Compute barycentric coordinates
            float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            // Check if point is in triangle
            if (u >= 0 && v >= 0 && (u + v) < 1) {
                return true;
            }

            return false;
        }

        public bool PointInRectangle(Vector2 x, Vector2 y, Vector2 z, Vector2 w, Vector2 pt) {
            if (PointInTriangle(x, y, z, pt)) return true;
            if (PointInTriangle(x, z, w, pt)) return true;
            return false;
        }

        public List<Vector3> RotateAroundPoint(List<Vector3> vertices, Vector3 rotation, Vector3 pivot) {
            // Create a quaternion rotation from our Euler angles
            Quaternion quatRot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
            Vector3 shift = pivot - quatRot * pivot;
            // Rotate each vertex
            for (int i = 0; i < vertices.Count; i++) {
                vertices[i] = quatRot * vertices[i] + shift;
            }

            return vertices;
        }
    }
}