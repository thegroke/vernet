using System;
using Xunit;

namespace dev.waynemarsh.vernet.Tests
{
  public class VerletSimpleTest
  {
    [Fact]
    public void FunctionalityMatchesRegular()
    {
      Random r = new Random();

      const float dt = 1f / 50;
      float v0 = (float)(100 * r.NextDouble());
      float v1 = (float)(100 * r.NextDouble());
      float d = 0.9f + (float)(0.1 * r.NextDouble());
      var regular = new Verletf(v0, v1, d);
      var simple = new VerletSimple(dt, v0, v1, d);

      for (var i = 0; i < 1000; ++i)
      {
        float a = (float)(10 * r.NextDouble());
        regular.Integrate(dt, a);
        simple.Integrate(a);
      }

      Assert.Equal(regular.Value, simple.Value);
      Assert.Equal(regular.DValue, simple.DValue);
    }

    [Fact]
    public void WrappedMatchesRegular()
    {
      Random r = new Random();

      const float dt = 1f / 50;
      float v0 = (float)(100 * r.NextDouble());
      float v1 = (float)(100 * r.NextDouble());
      float d = 0.9f + (float)(0.1 * r.NextDouble());
      var regular = new Verletf(v0, v1, d);
      var simpleWrapping = new VerletSimple(dt, new Verletf(v0, v1, d));

      for (var i = 0; i < 1000; ++i)
      {
        float a = (float)(10 * r.NextDouble());
        regular.Integrate(dt, a);
        simpleWrapping.Integrate(a);
      }

      Assert.Equal(regular.Value, simpleWrapping.Value);
      Assert.Equal(regular.DValue, simpleWrapping.DValue);
    }

    [Fact]
    public void ReadingSpeedIsAccurate()
    {
      const float dt = 1f / 50;
      var simple = new VerletSimple(dt, 4, 54, 1);

      Assert.Equal(50, simple.DValue);
      Assert.Equal(2500, simple.Speed);
    }

    [Fact]
    public void SettingSpeedIsAccurate()
    {
      const float dt = 1f / 50;
      var simple = new VerletSimple(dt);
      simple.Speed = 10;

      // One second
      for (int i = 0; i < 50; ++i)
      {
        simple.Integrate();
      }

      const float err = 50 * dt * dt;
      Assert.InRange(simple.Value, 10 - err, 10 + err);
    }
  }
}
