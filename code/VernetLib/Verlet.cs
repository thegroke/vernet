using System;
using System.Runtime.CompilerServices;

namespace dev.waynemarsh.vernet
{
  public class Verletf
  {
    float c, l;

    public float Value
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        return c;
      }
    }

    public float DValue
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        return c - l;
      }
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
        l = c - value;
      }
    }

    public Verletf()
    {
      c = l = 0;
    }

    public Verletf(float initialValue)
    {
      c = l = initialValue;
    }

    public Verletf(float v0, float v1)
    {
      l = v0;
      c = v1;
    }

    public float Integrate(float dt, float a)
    {
      float next = c + (c - l) + a * dt * dt;
      l = c;
      c = next;
      return c;
    }

    public static Verletf WarmStart(float dt, float v0, float u1, float a)
    {
      //  Velocity Verlet
      // position += timestep * (velocity + timestep * acceleration / 2);
      // velocity += timestep * (acceleration + newAcceleration) / 2;

      float u0 = u1 - dt * a;
      float vdiff = u0 * dt + 0.5f * a * dt * dt;

      return new Verletf(v0 - vdiff, v0);
    }
  }

}
