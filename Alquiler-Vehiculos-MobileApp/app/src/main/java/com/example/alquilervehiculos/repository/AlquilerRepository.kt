package com.example.alquilervehiculos.repository

import com.example.alquilervehiculos.data.model.AlquilerRequest
import com.example.alquilervehiculos.data.model.AlquilerResponse
import com.example.alquilervehiculos.network.ApiService

class AlquilerRepository(private val api: ApiService) {

    suspend fun crearAlquiler(dto: AlquilerRequest): AlquilerResponse {
        return api.crearAlquiler(dto)
    }
}