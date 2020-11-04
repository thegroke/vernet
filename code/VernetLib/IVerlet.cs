namespace dev.waynemarsh.vernet
{

  public interface IVerlet
  {
    float Value { get; set; }
    float DValue { get; set; }

    float Integrate(float dt, float a);
    float Integrate(float dt);
    void ApplyImpulse(float amount);
    void ZeroEnergy();
  }
}