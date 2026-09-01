using System;

// Claramente las reglas de negocio deben de estar en un enum o una clase y no Hardcodeadas
public class ValidadorCapacidadPago : ValidadorBase
{
    protected override bool ValidarPropio(SolicitudCredito solicitud)
    {
        // Regla simple: la cuota estimada no debe superar el 40% del ingreso 
        decimal cuotaEstimada = solicitud.MontoSolicitado * 0.05m;
        bool ok = cuotaEstimada <= solicitud.IngresoMensual * 0.4m;
        Console.WriteLine(ok ? "Capacidad de pago suficiente."
                              : "Capacidad de pago insuficiente.");
        return ok;
    }
}