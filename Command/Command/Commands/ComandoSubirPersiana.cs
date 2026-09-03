public class ComandoSubirPersiana : IComando
{
    private readonly Persiana _persiana;
    public ComandoSubirPersiana(Persiana persiana) => _persiana = persiana;

    public void Ejecutar() => _persiana.Subir();
    public void Deshacer() => _persiana.Bajar();
}