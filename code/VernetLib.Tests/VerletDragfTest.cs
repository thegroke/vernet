using System;
using dev.waynemarsh.vernet;
using Xunit;

namespace dev.waynemarsh.vernet.Tests
{
  public class VerletDragFTest
  {
    [Fact]
    public void RepeatedIntegration_Zero_StaysZero()
    {
      const int td = 50;
      const float dt = 1f / td;
      var v = new VerletDragf(1);

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(0, v.Value);
    }

    [Fact]
    public void InitialValue_DoesNotAffectSpeed()
    {
      var v = new VerletDragf(5, 1);

      Assert.Equal(0, v.DValue);
    }

    [Fact]
    public void SettingSpeed_ReadsBackCorrectly()
    {
      var v = new VerletDragf(5, 1);
      v.DValue = 8;
      Assert.Equal(8, v.DValue);
    }

    [Fact]
    public void RepeatedIntegration_Speed_StaysConstant()
    {
      const int td = 50;
      const float dt = 1f / td;
      const float u = 8;
      var v = new VerletDragf(5, 1);
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
      var v = new VerletDragf(5, 1);
      v.DValue = 8;

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(5 + 8 * 100, v.Value);
    }

    [Theory]
    [InlineData(500, 1.4f)]
    [InlineData(50000, 20)]
    [InlineData(10, 0.05f)]
    public void TerminalVelocity_Calculations_Accurate(float a, float terminalVelocity)
    {
      const float dt = 1f / 50;
      const int numSeconds = 1;
      const int iterations = 50 * numSeconds;

      var drag = VerletDragf.CalculateDragFactorForTerminalVelocity(
              dt,
              a,
              terminalVelocity
            );
      var v = new VerletDragf(drag);

      for (var i = 0; i < iterations; ++i)
      {
        v.Integrate(dt, a);
      }

      Assert.Equal(terminalVelocity, v.DValue, 2);
    }
  }
}
