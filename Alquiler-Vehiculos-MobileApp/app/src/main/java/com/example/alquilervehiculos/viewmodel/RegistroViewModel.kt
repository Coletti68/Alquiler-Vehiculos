package com.example.alquilervehiculos.viewmodel

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.alquilervehiculos.data.model.RegistroRequest
import com.example.alquilervehiculos.network.ApiService
import kotlinx.coroutines.launch
import java.io.IOException



class RegistroViewModel(private val api: ApiService) : ViewModel() {

    private val _registroExitoso = MutableLiveData<Boolean>()
    val registroExitoso: LiveData<Boolean> get() = _registroExitoso

    private val _mensajeError = MutableLiveData<String>()
    val mensajeError: LiveData<String> get() = _mensajeError

    fun registrarUsuario(data: RegistroRequest) {
        viewModelScope.launch {
            try {
                val response = api.registrarUsuario(data)
                if (response.isSuccessful) {
                    _registroExitoso.postValue(true)
                } else {
                    val errorBody = response.errorBody()?.string() ?: "Error desconocido del servidor"
                    _mensajeError.postValue(errorBody)
                }
            } catch (e: IOException) {
                _mensajeError.postValue("Error de red: ${e.localizedMessage}")
            } catch (e: Exception) {
                _mensajeError.postValue("Error inesperado: ${e.localizedMessage}")
            }
        }
    }
}