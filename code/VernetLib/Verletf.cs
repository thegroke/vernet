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

    public Verletf(float drag)
    {
      c = l = 0;
      d = drag;
    }

  public Verletf(float initialValue, float drag)
    {
      c = l = initialValue;
      d = drag;
    }

public Verletf(float v0, float v1, float drag)
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
