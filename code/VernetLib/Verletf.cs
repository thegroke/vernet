using System.Runtime.CompilerServices;

namespace dev.waynemarsh.vernet
{
  public class Verletf : IVerlet
  {
    private float c, l;
    private readonly float d;

    public float Value
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        return c;
      }
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
        c = value;
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
      d = 1;
    }

    public Verletf(float drag = 1) : this(0, 0, drag)
    { }

    public Verletf(float initialValue, float drag = 1) :
      this(initialValue, initialValue, drag)
    { }

    public Verletf(float v0, float v1, float drag = 1)
    {
      l = v0;
      c = v1;
      d = drag;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Integrate(float dt)
    {
      return Integrate(dt, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Integrate(float dt, float a)
    {
      float next = (1f + d) * c - d * l + a * dt * dt;
      l = c;
      c = next;

      return c;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ApplyImpulse(float amount)
    {
      l -= amount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ZeroEnergy()
    {
      l = c;
    }

    public static float CalculateDragFactorForTerminalVelocity(float dt, float a, float terminalVelocity)
    {
      return (terminalVelocity - a * dt * dt) / terminalVelocity;
    }
  }
}
