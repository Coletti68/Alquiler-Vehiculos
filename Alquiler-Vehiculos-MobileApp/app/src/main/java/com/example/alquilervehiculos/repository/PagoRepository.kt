package com.example.alquilervehiculos.repository

import com.example.alquilervehiculos.data.model.PagoRequest
import com.example.alquilervehiculos.network.ApiService
import retrofit2.Response

class PagoRepository(private val api: ApiService) {
    suspend fun crearPago(pago: PagoRequest): Response<Unit> {
        return api.registrarPago(pago)
    }
}