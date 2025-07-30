package com.example.alquilervehiculos.data.model

data class RegistroRequest(
    val nombreCompleto: String,
    val email: String,
    val password: String,
    val telefono: String,
    val dni: String,
    val direccion: String,
    val fechaNacimiento: String,
    val pais: String,
    val activo: Boolean = true

)