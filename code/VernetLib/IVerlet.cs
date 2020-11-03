namespace dev.waynemarsh.vernet
{

  public interface IVerlet
  {
    float Value { get; }
    float DValue { get; set; }

    float Integrate(float dt, float a);
  }
}