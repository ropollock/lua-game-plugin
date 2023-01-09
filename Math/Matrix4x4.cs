using System;

namespace Lou
{
  /// <summary>
  ///   <para>A standard 4x4 transformation matrix.</para>
  /// </summary>
  public struct Matrix4x4
  {
    public float m00;
    public float m10;
    public float m20;
    public float m30;
    public float m01;
    public float m11;
    public float m21;
    public float m31;
    public float m02;
    public float m12;
    public float m22;
    public float m32;
    public float m03;
    public float m13;
    public float m23;
    public float m33;

    public float this[int row, int column]
    {
      get
      {
        return this[row + column * 4];
      }
      set
      {
        this[row + column * 4] = value;
      }
    }

    public float this[int index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return this.m00;
          case 1:
            return this.m10;
          case 2:
            return this.m20;
          case 3:
            return this.m30;
          case 4:
            return this.m01;
          case 5:
            return this.m11;
          case 6:
            return this.m21;
          case 7:
            return this.m31;
          case 8:
            return this.m02;
          case 9:
            return this.m12;
          case 10:
            return this.m22;
          case 11:
            return this.m32;
          case 12:
            return this.m03;
          case 13:
            return this.m13;
          case 14:
            return this.m23;
          case 15:
            return this.m33;
          default:
            throw new IndexOutOfRangeException("Invalid matrix index!");
        }
      }
      set
      {
        switch (index)
        {
          case 0:
            this.m00 = value;
            break;
          case 1:
            this.m10 = value;
            break;
          case 2:
            this.m20 = value;
            break;
          case 3:
            this.m30 = value;
            break;
          case 4:
            this.m01 = value;
            break;
          case 5:
            this.m11 = value;
            break;
          case 6:
            this.m21 = value;
            break;
          case 7:
            this.m31 = value;
            break;
          case 8:
            this.m02 = value;
            break;
          case 9:
            this.m12 = value;
            break;
          case 10:
            this.m22 = value;
            break;
          case 11:
            this.m32 = value;
            break;
          case 12:
            this.m03 = value;
            break;
          case 13:
            this.m13 = value;
            break;
          case 14:
            this.m23 = value;
            break;
          case 15:
            this.m33 = value;
            break;
          default:
            throw new IndexOutOfRangeException("Invalid matrix index!");
        }
      }
    }

//    /// <summary>
//    ///   <para>The inverse of this matrix (Read Only).</para>
//    /// </summary>
//    public Matrix4x4 inverse
//    {
//      get
//      {
//        return Matrix4x4.Inverse(this);
//      }
//    }
//
//    /// <summary>
//    ///   <para>Returns the transpose of this matrix (Read Only).</para>
//    /// </summary>
//    public Matrix4x4 transpose
//    {
//      get
//      {
//        return Matrix4x4.Transpose(this);
//      }
//    }
//
//    /// <summary>
//    ///   <para>The determinant of the matrix.</para>
//    /// </summary>
//    public float determinant
//    {
//      get
//      {
//        return Matrix4x4.Determinant(this);
//      }
//    }

    /// <summary>
    ///   <para>Returns a matrix with all elements set to zero (Read Only).</para>
    /// </summary>
    public static Matrix4x4 zero
    {
      get
      {
        return new Matrix4x4() { m00 = 0.0f, m01 = 0.0f, m02 = 0.0f, m03 = 0.0f, m10 = 0.0f, m11 = 0.0f, m12 = 0.0f, m13 = 0.0f, m20 = 0.0f, m21 = 0.0f, m22 = 0.0f, m23 = 0.0f, m30 = 0.0f, m31 = 0.0f, m32 = 0.0f, m33 = 0.0f };
      }
    }

    /// <summary>
    ///   <para>Returns the identity matrix (Read Only).</para>
    /// </summary>
    public static Matrix4x4 identity
    {
      get
      {
        return new Matrix4x4() { m00 = 1f, m01 = 0.0f, m02 = 0.0f, m03 = 0.0f, m10 = 0.0f, m11 = 1f, m12 = 0.0f, m13 = 0.0f, m20 = 0.0f, m21 = 0.0f, m22 = 1f, m23 = 0.0f, m30 = 0.0f, m31 = 0.0f, m32 = 0.0f, m33 = 1f };
      }
    }

    public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
    {
      return new Matrix4x4() { m00 = (float) ((double) lhs.m00 * (double) rhs.m00 + (double) lhs.m01 * (double) rhs.m10 + (double) lhs.m02 * (double) rhs.m20 + (double) lhs.m03 * (double) rhs.m30), m01 = (float) ((double) lhs.m00 * (double) rhs.m01 + (double) lhs.m01 * (double) rhs.m11 + (double) lhs.m02 * (double) rhs.m21 + (double) lhs.m03 * (double) rhs.m31), m02 = (float) ((double) lhs.m00 * (double) rhs.m02 + (double) lhs.m01 * (double) rhs.m12 + (double) lhs.m02 * (double) rhs.m22 + (double) lhs.m03 * (double) rhs.m32), m03 = (float) ((double) lhs.m00 * (double) rhs.m03 + (double) lhs.m01 * (double) rhs.m13 + (double) lhs.m02 * (double) rhs.m23 + (double) lhs.m03 * (double) rhs.m33), m10 = (float) ((double) lhs.m10 * (double) rhs.m00 + (double) lhs.m11 * (double) rhs.m10 + (double) lhs.m12 * (double) rhs.m20 + (double) lhs.m13 * (double) rhs.m30), m11 = (float) ((double) lhs.m10 * (double) rhs.m01 + (double) lhs.m11 * (double) rhs.m11 + (double) lhs.m12 * (double) rhs.m21 + (double) lhs.m13 * (double) rhs.m31), m12 = (float) ((double) lhs.m10 * (double) rhs.m02 + (double) lhs.m11 * (double) rhs.m12 + (double) lhs.m12 * (double) rhs.m22 + (double) lhs.m13 * (double) rhs.m32), m13 = (float) ((double) lhs.m10 * (double) rhs.m03 + (double) lhs.m11 * (double) rhs.m13 + (double) lhs.m12 * (double) rhs.m23 + (double) lhs.m13 * (double) rhs.m33), m20 = (float) ((double) lhs.m20 * (double) rhs.m00 + (double) lhs.m21 * (double) rhs.m10 + (double) lhs.m22 * (double) rhs.m20 + (double) lhs.m23 * (double) rhs.m30), m21 = (float) ((double) lhs.m20 * (double) rhs.m01 + (double) lhs.m21 * (double) rhs.m11 + (double) lhs.m22 * (double) rhs.m21 + (double) lhs.m23 * (double) rhs.m31), m22 = (float) ((double) lhs.m20 * (double) rhs.m02 + (double) lhs.m21 * (double) rhs.m12 + (double) lhs.m22 * (double) rhs.m22 + (double) lhs.m23 * (double) rhs.m32), m23 = (float) ((double) lhs.m20 * (double) rhs.m03 + (double) lhs.m21 * (double) rhs.m13 + (double) lhs.m22 * (double) rhs.m23 + (double) lhs.m23 * (double) rhs.m33), m30 = (float) ((double) lhs.m30 * (double) rhs.m00 + (double) lhs.m31 * (double) rhs.m10 + (double) lhs.m32 * (double) rhs.m20 + (double) lhs.m33 * (double) rhs.m30), m31 = (float) ((double) lhs.m30 * (double) rhs.m01 + (double) lhs.m31 * (double) rhs.m11 + (double) lhs.m32 * (double) rhs.m21 + (double) lhs.m33 * (double) rhs.m31), m32 = (float) ((double) lhs.m30 * (double) rhs.m02 + (double) lhs.m31 * (double) rhs.m12 + (double) lhs.m32 * (double) rhs.m22 + (double) lhs.m33 * (double) rhs.m32), m33 = (float) ((double) lhs.m30 * (double) rhs.m03 + (double) lhs.m31 * (double) rhs.m13 + (double) lhs.m32 * (double) rhs.m23 + (double) lhs.m33 * (double) rhs.m33) };
    }

    public static Vector4 operator *(Matrix4x4 lhs, Vector4 v)
    {
      Vector4 vector4;
      vector4.x = (float) ((double) lhs.m00 * (double) v.x + (double) lhs.m01 * (double) v.y + (double) lhs.m02 * (double) v.z + (double) lhs.m03 * (double) v.w);
      vector4.y = (float) ((double) lhs.m10 * (double) v.x + (double) lhs.m11 * (double) v.y + (double) lhs.m12 * (double) v.z + (double) lhs.m13 * (double) v.w);
      vector4.z = (float) ((double) lhs.m20 * (double) v.x + (double) lhs.m21 * (double) v.y + (double) lhs.m22 * (double) v.z + (double) lhs.m23 * (double) v.w);
      vector4.w = (float) ((double) lhs.m30 * (double) v.x + (double) lhs.m31 * (double) v.y + (double) lhs.m32 * (double) v.z + (double) lhs.m33 * (double) v.w);
      return vector4;
    }

    public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
    {
      if (lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2))
        return lhs.GetColumn(3) == rhs.GetColumn(3);
      return false;
    }

    public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
    {
      return !(lhs == rhs);
    }

    public override int GetHashCode()
    {
      return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
    }

    public override bool Equals(object other)
    {
      if (!(other is Matrix4x4))
        return false;
      Matrix4x4 matrix4x4 = (Matrix4x4) other;
      if (this.GetColumn(0).Equals((object) matrix4x4.GetColumn(0)) && this.GetColumn(1).Equals((object) matrix4x4.GetColumn(1)) && this.GetColumn(2).Equals((object) matrix4x4.GetColumn(2)))
        return this.GetColumn(3).Equals((object) matrix4x4.GetColumn(3));
      return false;
    }

    /// <summary>
    ///   <para>Get a column of the matrix.</para>
    /// </summary>
    /// <param name="i"></param>
    public Vector4 GetColumn(int i)
    {
      return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
    }

    /// <summary>
    ///   <para>Returns a row of the matrix.</para>
    /// </summary>
    /// <param name="i"></param>
    public Vector4 GetRow(int i)
    {
      return new Vector4(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
    }

    /// <summary>
    ///   <para>Sets a column of the matrix.</para>
    /// </summary>
    /// <param name="i"></param>
    /// <param name="v"></param>
    public void SetColumn(int i, Vector4 v)
    {
      this[0, i] = v.x;
      this[1, i] = v.y;
      this[2, i] = v.z;
      this[3, i] = v.w;
    }

    /// <summary>
    ///   <para>Sets a row of the matrix.</para>
    /// </summary>
    /// <param name="i"></param>
    /// <param name="v"></param>
    public void SetRow(int i, Vector4 v)
    {
      this[i, 0] = v.x;
      this[i, 1] = v.y;
      this[i, 2] = v.z;
      this[i, 3] = v.w;
    }

    /// <summary>
    ///   <para>Transforms a position by this matrix (generic).</para>
    /// </summary>
    /// <param name="v"></param>
    public Vector3 MultiplyPoint(Vector3 v)
    {
      Vector3 vector3;
      vector3.x = (float) ((double) this.m00 * (double) v.x + (double) this.m01 * (double) v.y + (double) this.m02 * (double) v.z) + this.m03;
      vector3.y = (float) ((double) this.m10 * (double) v.x + (double) this.m11 * (double) v.y + (double) this.m12 * (double) v.z) + this.m13;
      vector3.z = (float) ((double) this.m20 * (double) v.x + (double) this.m21 * (double) v.y + (double) this.m22 * (double) v.z) + this.m23;
      float num = 1f / ((float) ((double) this.m30 * (double) v.x + (double) this.m31 * (double) v.y + (double) this.m32 * (double) v.z) + this.m33);
      vector3.x *= num;
      vector3.y *= num;
      vector3.z *= num;
      return vector3;
    }

    /// <summary>
    ///   <para>Transforms a position by this matrix (fast).</para>
    /// </summary>
    /// <param name="v"></param>
    public Vector3 MultiplyPoint3x4(Vector3 v)
    {
      Vector3 vector3;
      vector3.x = (float) ((double) this.m00 * (double) v.x + (double) this.m01 * (double) v.y + (double) this.m02 * (double) v.z) + this.m03;
      vector3.y = (float) ((double) this.m10 * (double) v.x + (double) this.m11 * (double) v.y + (double) this.m12 * (double) v.z) + this.m13;
      vector3.z = (float) ((double) this.m20 * (double) v.x + (double) this.m21 * (double) v.y + (double) this.m22 * (double) v.z) + this.m23;
      return vector3;
    }

    /// <summary>
    ///   <para>Transforms a direction by this matrix.</para>
    /// </summary>
    /// <param name="v"></param>
    public Vector3 MultiplyVector(Vector3 v)
    {
      Vector3 vector3;
      vector3.x = (float) ((double) this.m00 * (double) v.x + (double) this.m01 * (double) v.y + (double) this.m02 * (double) v.z);
      vector3.y = (float) ((double) this.m10 * (double) v.x + (double) this.m11 * (double) v.y + (double) this.m12 * (double) v.z);
      vector3.z = (float) ((double) this.m20 * (double) v.x + (double) this.m21 * (double) v.y + (double) this.m22 * (double) v.z);
      return vector3;
    }

    /// <summary>
    ///   <para>Creates a scaling matrix.</para>
    /// </summary>
    /// <param name="v"></param>
    public static Matrix4x4 Scale(Vector3 v)
    {
      return new Matrix4x4() { m00 = v.x, m01 = 0.0f, m02 = 0.0f, m03 = 0.0f, m10 = 0.0f, m11 = v.y, m12 = 0.0f, m13 = 0.0f, m20 = 0.0f, m21 = 0.0f, m22 = v.z, m23 = 0.0f, m30 = 0.0f, m31 = 0.0f, m32 = 0.0f, m33 = 1f };
    }

    /// <summary>
    ///   <para>Returns a nicely formatted string for this matrix.</para>
    /// </summary>
    /// <param name="format"></param>
    public override string ToString()
    {
      return $"{(object) this.m00:F5}\t{(object) this.m01:F5}\t{(object) this.m02:F5}\t{(object) this.m03:F5}\n{(object) this.m10:F5}\t{(object) this.m11:F5}\t{(object) this.m12:F5}\t{(object) this.m13:F5}\n{(object) this.m20:F5}\t{(object) this.m21:F5}\t{(object) this.m22:F5}\t{(object) this.m23:F5}\n{(object) this.m30:F5}\t{(object) this.m31:F5}\t{(object) this.m32:F5}\t{(object) this.m33:F5}\n";
    }

    /// <summary>
    ///   <para>Returns a nicely formatted string for this matrix.</para>
    /// </summary>
    /// <param name="format"></param>
    public string ToString(string format)
    {
      return
        $"{(object) this.m00.ToString(format)}\t{(object) this.m01.ToString(format)}\t{(object) this.m02.ToString(format)}\t{(object) this.m03.ToString(format)}\n{(object) this.m10.ToString(format)}\t{(object) this.m11.ToString(format)}\t{(object) this.m12.ToString(format)}\t{(object) this.m13.ToString(format)}\n{(object) this.m20.ToString(format)}\t{(object) this.m21.ToString(format)}\t{(object) this.m22.ToString(format)}\t{(object) this.m23.ToString(format)}\n{(object) this.m30.ToString(format)}\t{(object) this.m31.ToString(format)}\t{(object) this.m32.ToString(format)}\t{(object) this.m33.ToString(format)}\n";
    }
  }
}