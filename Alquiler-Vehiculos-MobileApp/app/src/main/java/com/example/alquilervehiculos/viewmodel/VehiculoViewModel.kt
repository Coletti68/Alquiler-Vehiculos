package com.example.alquilervehiculos.viewmodel

import android.util.Log
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.alquilervehiculos.data.model.Vehiculo
import com.example.alquilervehiculos.data.model.AlquilerRequest
import com.example.alquilervehiculos.data.model.AlquilerResponse
import com.example.alquilervehiculos.data.model.PagoRequest
import com.example.alquilervehiculos.network.RestClient
import com.example.alquilervehiculos.repository.AlquilerRepository
import com.example.alquilervehiculos.repository.PagoRepository
import com.google.gson.Gson
import kotlinx.coroutines.launch
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale

class VehiculoViewModel : ViewModel() {

    val vehiculos = MutableLiveData<List<Vehiculo>>()
    val isLoading = MutableLiveData<Boolean>()
    val error = MutableLiveData<String?>()

    // 🆕 Estas 3 líneas son las que te faltaban:
    private val repo = AlquilerRepository(RestClient.api)
    val alquilerResultado = MutableLiveData<AlquilerResponse?>()
    val alquilerError = MutableLiveData<String?>()

    fun cargarVehiculos() {
        isLoading.value = true
        error.value = null

        viewModelScope.launch {
            try {
                val response = RestClient.api.obtenerVehiculos()
                if (response.isSuccessful) {
                    val lista = response.body() ?: emptyList()
                    Log.d("VehiculoViewModel", "Vehículos recibidos: ${lista.size}")
                    vehiculos.postValue(lista)
                } else {
                    error.postValue("Error al cargar vehículos: ${response.code()}")
                }
            } catch (e: Exception) {
                error.postValue("Excepción: ${e.localizedMessage}")
            } finally {
                isLoading.postValue(false)
            }
        }
    }

    fun crearAlquiler(dto: AlquilerRequest, metodo: String, monto: Double, callback: (Boolean, String) -> Unit) {
        viewModelScope.launch {
            try {
                val alquiler = repo.crearAlquiler(dto)
                alquilerResultado.postValue(alquiler)

                val pago = PagoRequest(
                    alquilerId = alquiler.alquilerId,
                    monto = monto,
                    metodoPago = metodo,
                    fechaPago = obtenerAhoraEnIso()
                )

                val gson = Gson()
                Log.d("JSON_PAGO_ENVIADO", gson.toJson(pago))

                val pagoRepo = PagoRepository(RestClient.api)
                val respuestaPago = pagoRepo.crearPago(pago)

                val errorBody = respuestaPago.errorBody()?.string()
                Log.d("PAGO_RESPONSE", "Código=${respuestaPago.code()} / Msg=${respuestaPago.message()}")
                Log.d("PAGO_ERROR_BODY", errorBody ?: "Sin contenido")

                if (respuestaPago.isSuccessful) {
                    callback(true, "✔️ Alquiler y pago registrados")
                } else {
                    callback(false, "⛔ Error del backend: ${errorBody ?: respuestaPago.message()}")
                }
            } catch (e: Exception) {
                callback(false, e.message ?: "Error desconocido")
            }
        }
    }
    }


fun obtenerAhoraEnIso(): String {
    val sdf = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.getDefault())
    return sdf.format(Date())
}