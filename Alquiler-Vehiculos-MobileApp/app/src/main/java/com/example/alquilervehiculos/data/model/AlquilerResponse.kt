package com.example.alquilervehiculos.data.model
import com.example.alquilervehiculos.data.model.Vehiculo

data class AlquilerResponse(
    val alquilerId: Int,
    val vehiculo: Vehiculo, // 👈 usás tu clase Vehiculo ya creada
    val fechaInicio: String,
    val fechaFin: String?,
    val total: Double,
    val estado: String,
    val aceptoTerminos: Boolean
)
