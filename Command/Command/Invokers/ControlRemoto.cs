using System;
using System.Collections.Generic;

// ---------- Invocador ----------
public class ControlRemoto
{
    private readonly Dictionary<string, IComando> _botones = new();
    private readonly Stack<IComando> _historial = new();

    public void AsignarBoton(string nombreBoton, IComando comando)
        => _botones[nombreBoton] = comando;

    public void PresionarBoton(string nombreBoton)
    {
        if (!_botones.TryGetValue(nombreBoton, out var comando))
        {
            Console.WriteLine($"El boton '{nombreBoton}' no tiene comando asignado.");
            return;
        }

        comando.Ejecutar();
        _historial.Push(comando);
    }

    public void DeshacerUltimo()
    {
        if (_historial.Count == 0)
        {
            Console.WriteLine("No hay acciones para deshacer.");
            return;
        }

        var ultimoComando = _historial.Pop();
        ultimoComando.Deshacer();
    }
}