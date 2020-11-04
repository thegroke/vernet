using Xunit;

namespace dev.waynemarsh.vernet.Tests
{
  public class VerletfTest
  {
    [Fact]
    public void RepeatedIntegration_Zero_StaysZero()
    {
      const int td = 50;
      const float dt = 1f / td;
      var v = new Verletf(1);

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(0, v.Value);
    }

    [Fact]
    public void InitialValue_DoesNotAffectSpeed()
    {
      var v = new Verletf(5, 1);

      Assert.Equal(0, v.DValue);
    }

    [Fact]
    public void SettingSpeed_ReadsBackCorrectly()
    {
      var v = new Verletf(5, 1);
      v.DValue = 8;
      Assert.Equal(8, v.DValue);
    }

    [Fact]
    public void ImpulseTakesEffect()
    {
      const int td = 50;
      const float dt = 1f / td;
      var v = new Verletf();

      v.ApplyImpulse(5 * dt);

      for (var i = 0; i < td; ++i)
      {
        v.Integrate(dt);
      }

      float err = td * dt * dt;
      Assert.InRange(v.Value, 5 - err, 5 + err);
    }

    [Fact]
    public void RepeatedIntegration_Speed_StaysConstant()
    {
      const int td = 50;
      const float dt = 1f / td;
      const float u = 8;
      var v = new Verletf(5, 1);
      v.DValue = u;

      for (var i = 0; i < 100; ++i)
      {
        v.Integrate(dt, 0);
      }

      Assert.Equal(u, v.DValue);
    }

    [Fact]
    public void Zeroing_Energy_Works()
    {
      const float dt = 1f / 50;

      var v = new Verletf();

      v.Integrate(dt, 100);

      Assert.NotEqual(0, v.DValue);

      var value = v.Value;

      v.ZeroEnergy();

      Assert.Equal(0, v.DValue);
      Assert.Equal(value, v.Value);
    }

    [Fact]
    public void RepeatedIntegration_Speed_CorrectValue()
    {
      const float dt = 1f / 50;
      var v = new Verletf(5, 1);
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

      var drag = Verletf.CalculateDragFactorForTerminalVelocity(
              dt,
              a,
              terminalVelocity
            );
      var v = new Verletf(drag);

      for (var i = 0; i < iterations; ++i)
      {
        v.Integrate(dt, a);
      }

      Assert.Equal(terminalVelocity, v.DValue, 2);
    }
  }
}
