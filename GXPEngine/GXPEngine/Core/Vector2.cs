using System;

namespace GXPEngine.Core
{
	public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float pX = 0, float pY = 0)
        {
            x = pX;
            y = pY;
        }

        public Vector2(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        public float Length()
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }

        public static Vector2 operator *(float factor, Vector2 right)
        {
            return new Vector2(factor * right.x, factor * right.y);
        }

        public static Vector2 operator *(Vector2 left, float factor)
        {
            return new Vector2(factor * left.x, factor * left.y);
        }

        public static float Rad2Deg(float radians)
        {
            return radians * 180 / Mathf.PI;
        }

        public static float Deg2Rad(float degrees)
        {

            return (float)(Math.PI / 180) * degrees;
        }

        public static Vector2 GetUnitVectorRad(float radians)
        {
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }

        public static Vector2 GetUnitVectorDeg(float degrees)
        {
            return new Vector2(GetUnitVectorRad(Deg2Rad(degrees)));
        }

        public static Vector2 RandomUnitVector()
        {
            return GetUnitVectorRad(Utils.Random(0, 2 * Mathf.PI));
        }


        public Vector2 Normalized()
        {
            float length = Length();
            float pX = x;
            float pY = y;
            if (!(length > 0)) return new Vector2(pX, pY);
            pX /= length;
            pY /= length;
            return new Vector2(pX, pY);
        }

        public Vector2 Normalize()
        {
            float length = Length();
            if (!(length > 0)) return this;
            x /= length;
            y /= length;
            return this;
        }

        public Vector2 SetXy(float px, float py)
        {
            this.x = px;
            this.y = py;
            return this;
        }

        public Vector2 SetAngleRadians(float radians)
        {
            var length = Length();
            x = Mathf.Cos(radians) * length;
            y = Mathf.Sin(radians) * length;
            return this;
        }

        public float GetAngleRadians()
        {
            if (x == 0 && y == 0) return 0;
            return Mathf.Atan2(y, x);
        }

        public Vector2 SetAngleDegrees(float degrees)
        {
            return SetAngleRadians(Deg2Rad(degrees));
        }

        public float GetAngleDegrees()
        {
            return Rad2Deg(GetAngleRadians());
        }

        public Vector2 RotateDegrees(float degrees)
        {
            return RotateRadians(Deg2Rad(degrees));
        }

        public Vector2 RotateRadians(float radians)
        {

            Vector2 vector = new Vector2(x, y);
            x = vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians);
            y = vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians);
            return this;
        }

        public Vector2 RotateAroundDegrees(float degrees, Vector2 point)
        {
            x -= point.x;
            y -= point.y;
            RotateDegrees(degrees);
            x += point.x;
            y += point.y;
            return this;
        }

        public Vector2 RotateAroundRadians(float radians, Vector2 point)
        {
            return RotateAroundDegrees(Rad2Deg(radians), point);
        }

        public Vector2 Normal()
        {
            Vector2 vec = Normalized();
            return new Vector2(vec.y * -1, vec.x);
        }

        public float Dot(Vector2 b)
        {
            Vector2 a = this;
            return a.x * b.x + a.y * b.y;
        }

        /// <summary>
        ///  Reflect this vector into the provided vector
        /// </summary>
        /// <param name="bounce"></param>
        /// <param name="normal"></param>
        public void Reflect(float bounce, Vector2 normal)
        {
            Vector2 resultVec = this - (1 + bounce) * (Dot(normal)) * normal;
            this = resultVec;
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }
    }
}


