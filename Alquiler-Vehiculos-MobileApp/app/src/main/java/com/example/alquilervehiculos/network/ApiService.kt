package com.example.alquilervehiculos.network

import com.example.alquilervehiculos.data.model.LoginRequest
import com.example.alquilervehiculos.data.model.LoginResponse
import com.example.alquilervehiculos.data.model.RegistroRequest
import com.example.alquilervehiculos.data.model.AlquilerRequest
import com.example.alquilervehiculos.data.model.AlquilerResponse
import com.example.alquilervehiculos.data.model.PagoRequest
import com.example.alquilervehiculos.data.model.Vehiculo
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path

interface ApiService {
    @POST("Auth/login")
    suspend fun login(@Body request: LoginRequest): Response<LoginResponse>

    @POST("Usuario")
    suspend fun registrarUsuario(@Body registro: RegistroRequest): Response<Unit>

    @GET("Vehiculos")
    suspend fun obtenerVehiculos(): Response<List<Vehiculo>>

    @POST("Alquiler")
    suspend fun crearAlquiler(@Body alquiler: AlquilerRequest): AlquilerResponse

    @POST("Pago")
    suspend fun registrarPago(@Body pago: PagoRequest): Response<Unit>

    @GET("alquiler/usuario/{id}/resumen")
    suspend fun obtenerResumenPorUsuario(@Path("id") usuarioId: Int): List<AlquilerResponse>

    /*//@GET("api/Vehiculos/{id}")
    suspend fun getVehiculoById(@Path("id") id: Int): Vehiculo

    @POST("api/Vehiculos")
    suspend fun addVehiculo(@Body vehiculo: Vehiculo): Vehiculo

    @PUT("api/Vehiculos/{id}")
    suspend fun updateVehiculo(@Path("id") id: Int, @Body vehiculo: Vehiculo): Vehiculo

    @DELETE("api/Vehiculos/{id}")
    suspend fun deleteVehiculo(@Path("id") id: Int)
    */


}