public class ComandoBajarPersiana : IComando
{
    private readonly Persiana _persiana;
    public ComandoBajarPersiana(Persiana persiana) => _persiana = persiana;

    public void Ejecutar() => _persiana.Bajar();
    public void Deshacer() => _persiana.Subir();
}