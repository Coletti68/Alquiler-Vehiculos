package com.example.alquilervehiculos.ui.auth.ui.login

import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import com.example.alquilervehiculos.databinding.ActivityLogin2Binding
import com.example.alquilervehiculos.viewmodel.AuthViewModel
import android.content.Intent
import com.example.alquilervehiculos.ui.auth.ui.home.HomeActivity
import com.example.alquilervehiculos.ui.auth.ui.register.RegistroActivity

class LoginActivity : AppCompatActivity() {

    private lateinit var binding: ActivityLogin2Binding
    private val viewModel: AuthViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        println("Iniciando Login Activity")
        binding = ActivityLogin2Binding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.btnLogin.setOnClickListener {
            val email = binding.etEmail.text.toString().trim()
            val password = binding.etPassword.text.toString().trim()

            println("Login presionado: email=$email / pass=$password")
            viewModel.login(email, password)
        }

        binding.tvRegistrarse.setOnClickListener {
            val intent = Intent(this, RegistroActivity::class.java)
            startActivity(intent)
        }

        viewModel.loginResult.observe(this) { result ->
            if (result != null) {
                saveTokenYUsuario(result.token, result.usuarioId)
                Toast.makeText(this, "Bienvenido", Toast.LENGTH_SHORT).show()
                startActivity(Intent(this, HomeActivity::class.java))
                finish()
            } else {
                Toast.makeText(this, "Email o contraseña incorrectos", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun saveTokenYUsuario(token: String, usuarioId: Int) {
        val prefs = getSharedPreferences("auth", MODE_PRIVATE)
        prefs.edit()
            .putString("token", token)
            .putInt("usuarioId", usuarioId)
            .apply()
    }
}