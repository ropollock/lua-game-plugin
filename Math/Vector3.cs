using System;

namespace Lou {
    public struct Vector3 {
        public const float kEpsilon = 1E-05f;

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public float x;

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public float y;

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public float z;

        public float this[int index] {
            get {
                switch (index) {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
            set {
                switch (index) {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Vector3 normalized {
            get { return Vector3.Normalize(this); }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude {
            get {
                return Mathf.Sqrt((float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y +
                                           (double) this.z * (double) this.z));
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude {
            get {
                return (float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y +
                                (double) this.z * (double) this.z);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 0, 0).</para>
        /// </summary>
        public static Vector3 zero {
            get { return new Vector3(0.0f, 0.0f, 0.0f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(1, 1, 1).</para>
        /// </summary>
        public static Vector3 one {
            get { return new Vector3(1f, 1f, 1f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 0, 1).</para>
        /// </summary>
        public static Vector3 forward {
            get { return new Vector3(0.0f, 0.0f, 1f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 0, -1).</para>
        /// </summary>
        public static Vector3 back {
            get { return new Vector3(0.0f, 0.0f, -1f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 1, 0).</para>
        /// </summary>
        public static Vector3 up {
            get { return new Vector3(0.0f, 1f, 0.0f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, -1, 0).</para>
        /// </summary>
        public static Vector3 down {
            get { return new Vector3(0.0f, -1f, 0.0f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(-1, 0, 0).</para>
        /// </summary>
        public static Vector3 left {
            get { return new Vector3(-1f, 0.0f, 0.0f); }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(1, 0, 0).</para>
        /// </summary>
        public static Vector3 right {
            get { return new Vector3(1f, 0.0f, 0.0f); }
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y components and sets z to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector3(float x, float y) {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b) {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b) {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a) {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public static Vector3 operator *(Vector3 a, float d) {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator *(float d, Vector3 a) {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator /(Vector3 a, float d) {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs) {
            return (double) Vector3.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs) {
            return (double) Vector3.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
            t = Mathf.Clamp01(t);
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t) {
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Moves a point current in a straight line towards a target point.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta) {
            Vector3 vector3 = target - current;
            float magnitude = vector3.magnitude;
            if ((double) magnitude <= (double) maxDistanceDelta || (double) magnitude == 0.0)
                return target;
            return current + vector3 / magnitude * maxDistanceDelta;
        }

        public static Vector3 RotateTo(Vector3 from, Vector3 to, float angle)
        {
            if (Angle(from, to) == 0)
            {
                return from;
            }

            Vector3 n = Cross(from, to);

            n.Normalize();

            Matrix4x4 rotateMatrix = new Matrix4x4();

            double radian = angle * Mathf.PI / 180;
            float cosAngle = (float)Mathf.Cos((float)radian);
            float sinAngle = (float)Mathf.Sin((float)radian);

            rotateMatrix.SetRow(0, new Vector4(n.x * n.x * (1 - cosAngle) + cosAngle, n.x * n.y * (1 - cosAngle) + n.z * sinAngle, n.x * n.z * (1 - cosAngle) - n.y * sinAngle, 0));
            rotateMatrix.SetRow(1, new Vector4(n.x * n.y * (1 - cosAngle) - n.z * sinAngle, n.y * n.y * (1 - cosAngle) + cosAngle, n.y * n.z * (1 - cosAngle) + n.x * sinAngle, 0));
            rotateMatrix.SetRow(2, new Vector4(n.x * n.z * (1 - cosAngle) + n.y * sinAngle, n.y * n.z * (1 - cosAngle) - n.x * sinAngle, n.z * n.z * (1 - cosAngle) + cosAngle, 0));
            rotateMatrix.SetRow(3, new Vector4(0, 0, 0, 1));

            Vector4 v = from;
            Vector3 vector = new Vector3();
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; j++)
                {
                    vector[i] += v[j] * rotateMatrix[j, i];
                }
            }
            return vector;
        }

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime,
            float maxSpeed, float deltaTime) {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float) (1.0 / (1.0 + (double) num2 + 0.479999989271164 * (double) num2 * (double) num2 +
                                         0.234999999403954 * (double) num2 * (double) num2 * (double) num2));
            Vector3 vector = current - target;
            Vector3 vector3_1 = target;
            float maxLength = maxSpeed * smoothTime;
            Vector3 vector3_2 = Vector3.ClampMagnitude(vector, maxLength);
            target = current - vector3_2;
            Vector3 vector3_3 = (currentVelocity + num1 * vector3_2) * deltaTime;
            currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
            Vector3 vector3_4 = target + (vector3_2 + vector3_3) * num3;
            if ((double) Vector3.Dot(vector3_1 - current, vector3_4 - vector3_1) > 0.0) {
                vector3_4 = vector3_1;
                currentVelocity = (vector3_4 - vector3_1) / deltaTime;
            }

            return vector3_4;
        }

        /// <summary>
        ///   <para>Set x, y and z components of an existing Vector3.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        public void Set(float new_x, float new_y, float new_z) {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector3 Scale(Vector3 a, Vector3 b) {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector3 scale) {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs) {
            return new Vector3((float) ((double) lhs.y * (double) rhs.z - (double) lhs.z * (double) rhs.y),
                (float) ((double) lhs.z * (double) rhs.x - (double) lhs.x * (double) rhs.z),
                (float) ((double) lhs.x * (double) rhs.y - (double) lhs.y * (double) rhs.x));
        }

        public override int GetHashCode() {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override bool Equals(object other) {
            if (!(other is Vector3))
                return false;
            Vector3 vector3 = (Vector3) other;
            if (this.x.Equals(vector3.x) && this.y.Equals(vector3.y))
                return this.z.Equals(vector3.z);
            return false;
        }

        /// <summary>
        ///   <para>Reflects a vector off the plane defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal) {
            return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="value"></param>
        public static Vector3 Normalize(Vector3 value) {
            float num = Vector3.Magnitude(value);
            if ((double) num > 9.99999974737875E-06)
                return value / num;
            return Vector3.zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize() {
            float num = Vector3.Magnitude(this);
            if ((double) num > 9.99999974737875E-06)
                this = this / num;
            else
                this = Vector3.zero;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString() {
            return $"x: {(object) this.x}, y: {(object) this.y}, z: {(object) this.z}";
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector3 lhs, Vector3 rhs) {
            return (float) ((double) lhs.x * (double) rhs.x + (double) lhs.y * (double) rhs.y +
                            (double) lhs.z * (double) rhs.z);
        }

        /// <summary>
        ///   <para>Projects a vector onto another vector.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        public static Vector3 Project(Vector3 vector, Vector3 onNormal) {
            float num = Vector3.Dot(onNormal, onNormal);
            if ((double) num < (double) Mathf.Epsilon)
                return Vector3.zero;
            return onNormal * Vector3.Dot(vector, onNormal) / num;
        }

        /// <summary>
        ///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal) {
            return vector - Vector3.Project(vector, planeNormal);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The angle extends round from this vector.</param>
        /// <param name="to">The angle extends round to this vector.</param>
        public static float Angle(Vector3 from, Vector3 to) {
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector3 a, Vector3 b) {
            Vector3 vector3 = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return Mathf.Sqrt((float) ((double) vector3.x * (double) vector3.x +
                                       (double) vector3.y * (double) vector3.y +
                                       (double) vector3.z * (double) vector3.z));
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength) {
            if ((double) vector.sqrMagnitude > (double) maxLength * (double) maxLength)
                return vector.normalized * maxLength;
            return vector;
        }

        public static float Magnitude(Vector3 a) {
            return Mathf.Sqrt((float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y +
                                       (double) a.z * (double) a.z));
        }

        public static float SqrMagnitude(Vector3 a) {
            return (float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y + (double) a.z * (double) a.z);
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Min(Vector3 lhs, Vector3 rhs) {
            return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Max(Vector3 lhs, Vector3 rhs) {
            return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }
    }
}