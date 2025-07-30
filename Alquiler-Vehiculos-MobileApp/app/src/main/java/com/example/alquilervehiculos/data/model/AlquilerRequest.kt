package com.example.alquilervehiculos.data.model

data class AlquilerRequest(
    val usuarioId: Int,
    val vehiculoId: Int,
    val empleadoId: Int = 1, // o traelo desde prefs si lo tenés
    val fechaInicio: String,
    val fechaFin: String,
    val estado: String = "Pendiente",
    val aceptoTerminos: Boolean = true
)