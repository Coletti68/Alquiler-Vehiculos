package com.example.alquilervehiculos.data.model
import java.io.Serializable


data class Vehiculo(
    val vehiculoId: Int,
    val marca: String,
    val modelo: String,
    val anio: Int,
    val placa: String,
    val color: String,
    val tipo: String,
    val precioPorDia: Double,
    val estado: String
): Serializable

