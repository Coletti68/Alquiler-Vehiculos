package com.example.alquilervehiculos.data.model

data class LoginResponse(
    val token: String,
    val expira: String,
    val usuarioId: Int
)