package com.example.alquilervehiculos.viewmodel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.alquilervehiculos.data.model.LoginRequest
import com.example.alquilervehiculos.data.model.LoginResponse

import com.example.alquilervehiculos.data.model.RegistroRequest
import com.example.alquilervehiculos.network.RestClient
import kotlinx.coroutines.launch

class AuthViewModel : ViewModel() {

    val loginResult = MutableLiveData<LoginResponse?>()
    val registroSuccess = MutableLiveData<Boolean>()

    fun login(email: String, password: String) {
        val request = LoginRequest(email.trim(), password)
        println(" Enviando login: ${request.email} / ${request.password}")

        viewModelScope.launch {
            try {
                val response = RestClient.api.login(request)
                println(" Respuesta: código ${response.code()} - body=${response.body()}")

                if (response.isSuccessful) {
                    loginResult.postValue(response.body())
                } else {
                    println(" Error login: ${response.errorBody()?.string()}")
                    loginResult.postValue(null)
                }
            } catch (e: Exception) {
                println(" Excepción login: ${e.localizedMessage}")
                loginResult.postValue(null)
            }
        }
    }
}