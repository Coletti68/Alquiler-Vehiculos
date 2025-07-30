package com.example.alquilervehiculos.data.model

import com.google.gson.annotations.SerializedName

data class PagoRequest(
    @SerializedName("AlquilerId") val alquilerId: Int,
    @SerializedName("Monto") val monto: Double,
    @SerializedName("MetodoPago") val metodoPago: String,
    @SerializedName("FechaPago") val fechaPago: String
)