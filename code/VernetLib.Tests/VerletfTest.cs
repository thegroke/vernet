using System;
using dev.waynemarsh.vernet;
using Xunit;

namespace dev.waynemarsh.vernet.Tests
{
  public class VerletFTest
  {
    [Fact]
    public void RepeatedIntegration_Zero_StaysZero()
    {
      const int td = 50;
      const float dt = 1f / td;
      var v = new Verletf();

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(0, v.Value);
    }

    [Fact]
    public void InitialValue_DoesNotAffectSpeed()
    {
      var v = new Verletf(5);

      Assert.Equal(0, v.DValue);
    }

    [Fact]
    public void SettingSpeed_ReadsBackCorrectly()
    {
      var v = new Verletf(5);
      v.DValue = 8;
      Assert.Equal(8, v.DValue);
    }

    [Fact]
    public void RepeatedIntegration_Speed_StaysConstant()
    {
      const int td = 50;
      const float dt = 1f / td;
      const float u = 8;
      var v = new Verletf(5);
      v.DValue = u;

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(u, v.DValue);
    }

    [Fact]
    public void RepeatedIntegration_Speed_CorrectValue()
    {
      const float dt = 1f / 50;
      var v = new Verletf(5);
      v.DValue = 8;

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(5 + 8 * 100, v.Value);
    }

    [Fact]
    public void Integrating_ConstantAcceleration_WarmStart_WithinError()
    {
      const int td = 50;
      const float dt = 1f / td;
      const float u = 50;
      const float initialValue = 5;
      const float a = 60;
      const int t = 3;

      var v = Verletf.WarmStart(dt, initialValue, u, a);

      Assert.Equal(initialValue, v.Value);

      const int iterations = t * td;
      for (var i = 0; i < iterations; ++i)
      {
        v.Integrate(dt, a);
      }

      // error = O(dt^2)
      const float err = dt * dt * (1 + iterations);  // 1+ to account for velocity verlet error

      // speed = u + at
      const float expectedSpeed = (u + a * t) * dt;
      Assert.InRange(v.DValue, expectedSpeed - err, expectedSpeed + err);

      // s = ut + 1/2 at^2
      const float expectedValue = initialValue + u * t + 0.5f * a * t * t;
      Assert.InRange(v.Value, expectedValue - err, expectedValue + err);
    }
  }
}
