using System;

// ---------- Receptores: contienen la lógica real ----------
public class Luz
{
    public string Ubicacion { get; }
    public Luz(string ubicacion) => Ubicacion = ubicacion;

    public void Encender() => Console.WriteLine($"Luz de {Ubicacion}: encendida.");
    public void Apagar() => Console.WriteLine($"Luz de {Ubicacion}: apagada.");
}