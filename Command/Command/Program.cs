using System;

// ---------- Cliente ----------
public class Program
{
    public static void Main()
    {
        // El cliente crea los receptores y los comandos, y los conecta al invocador
        var luzSala = new Luz("Sala");
        var persianaSala = new Persiana("Sala");

        var control = new ControlRemoto();
        control.AsignarBoton("boton1_on", new ComandoEncenderLuz(luzSala));
        control.AsignarBoton("boton1_off", new ComandoApagarLuz(luzSala));
        control.AsignarBoton("boton2_subir", new ComandoSubirPersiana(persianaSala));
        control.AsignarBoton("boton2_bajar", new ComandoBajarPersiana(persianaSala));

        control.PresionarBoton("boton1_on");     // Luz de Sala: encendida.
        control.PresionarBoton("boton2_subir");  // Persiana de Sala: subida.

        Console.WriteLine("\n--- Deshaciendo última acción ---");
        control.DeshacerUltimo();                // Persiana de Sala: bajada.
    }
}