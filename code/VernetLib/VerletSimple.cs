using System.Runtime.CompilerServices;

namespace dev.waynemarsh.vernet
{
  public class VerletSimple
  {
    private readonly Verletf _impl;
    private readonly float dt;
    private readonly float dtinv;

    public float Value
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        return _impl.Value;
      }
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
        _impl.Value = value;
      }
    }

    public float DValue
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        return _impl.DValue;
      }
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
        _impl.DValue = value;
      }
    }

    public float Speed
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        return _impl.DValue * dtinv;
      }
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
        _impl.DValue = value * dt;
      }
    }

    public VerletSimple(float dt, Verletf toWrap)
    {
      _impl = toWrap;
      this.dt = dt;
      this.dtinv = 1f / dt;
    }

    public VerletSimple(float dt) :
     this(dt, 0, 0, 1)
    { }

    public VerletSimple(float dt, float initialValue) :
     this(dt, initialValue, initialValue, 1)
    { }

    public VerletSimple(float dt, float initialValue, float drag) :
     this(dt, initialValue, initialValue, drag)
    { }

    public VerletSimple(float dt, float v0, float v1, float drag)
    {
      _impl = new Verletf(v0, v1, drag);
      this.dt = dt;
      this.dtinv = 1f / dt;
    }

    public float Integrate()
    {
      return this.Integrate(0);
    }

    public float Integrate(float a)
    {
      return _impl.Integrate(dt, a);
    }
  }
}