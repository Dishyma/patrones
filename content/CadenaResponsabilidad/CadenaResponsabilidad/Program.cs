using System;

// ---------- Cliente ----------
public class Program
{
    public static void Main()
    {
        // Se arma la cadena una sola vez
        var datos = new ValidadorDatosPersonales();
        var historial = new ValidadorHistorialCrediticio();
        var capacidad = new ValidadorCapacidadPago();
        var monto = new ValidadorMontoMaximo();

        // Aquí establecemos los pasos
        datos.EstablecerSiguiente(historial)
             .EstablecerSiguiente(capacidad)
             .EstablecerSiguiente(monto);

        var solicitud = new SolicitudCredito
        {
            Nombre = "Laura Restrepo",
            Cedula = "1099887766",
            PuntajeCrediticio = 680,
            IngresoMensual = 4_000_000m,
            MontoSolicitado = 20_000_000m
        };

        Console.WriteLine($"--- Evaluando solicitud de {solicitud.Nombre} ---");
        bool aprobada = datos.Validar(solicitud);

        Console.WriteLine(aprobada ? "\nCrédito APROBADO." : "\nCrédito RECHAZADO.");
    }
}