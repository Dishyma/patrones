using System;

public class Persiana
{
    public string Ubicacion { get; }
    public Persiana(string ubicacion) => Ubicacion = ubicacion;

    public void Subir() => Console.WriteLine($"Persiana de {Ubicacion}: subida.");
    public void Bajar() => Console.WriteLine($"Persiana de {Ubicacion}: bajada.");
}