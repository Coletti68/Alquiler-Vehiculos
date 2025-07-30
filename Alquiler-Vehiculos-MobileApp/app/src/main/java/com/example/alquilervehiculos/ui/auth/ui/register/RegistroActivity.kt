package com.example.alquilervehiculos.ui.auth.ui.register

import android.app.AlertDialog
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.alquilervehiculos.data.model.RegistroRequest
import com.example.alquilervehiculos.databinding.ActivityRegistroBinding
import com.example.alquilervehiculos.network.RestClient
import com.example.alquilervehiculos.ui.auth.ui.login.LoginActivity
import com.example.alquilervehiculos.viewmodel.RegistroViewModel


class RegistroActivity : AppCompatActivity() {

    private lateinit var binding: ActivityRegistroBinding
    private lateinit var viewModel: RegistroViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        Log.d("Registro", "RegistroActivity cargada")
        binding = ActivityRegistroBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Cargamos países en el spinner
        val paises = listOf("Argentina", "Brasil", "Chile", "Paraguay")
        val adapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, paises)
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.spPais.adapter = adapter

        // Inicializamos ViewModel
        val api = RestClient.api
        viewModel = RegistroViewModel(api)

        binding.btnRegistrarse.setOnClickListener {
            Log.d("Registro", "Clic detectado")

            val paisSeleccionado = binding.spPais.selectedItem.toString()
            val dniIngresado = binding.etDni.text.toString()

            if (!validarDniLocal(paisSeleccionado, dniIngresado)) {
                Toast.makeText(this, "DNI inválido para $paisSeleccionado", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val datos = RegistroRequest(
                nombreCompleto = binding.etNombre.text.toString(),
                email = binding.etEmail.text.toString(),
                password = binding.etPassword.text.toString(),
                telefono = binding.etTelefono.text.toString(),
                dni = binding.etDni.text.toString(),
                direccion = binding.etDireccion.text.toString(),
                fechaNacimiento = binding.etFecha.text.toString(),
                pais = binding.spPais.selectedItem.toString(),
                activo = true
            )


            viewModel.registrarUsuario(datos)
        }

        viewModel.registroExitoso.observe(this) {
            AlertDialog.Builder(this)
                .setTitle("¡Registro exitoso! 🎉")
                .setMessage("Tu cuenta fue creada correctamente. ¿Querés iniciar sesión ahora?")
                .setPositiveButton("Sí") { _, _ ->
                    startActivity(Intent(this, LoginActivity::class.java))
                    finish()
                }
                .setNegativeButton("Más tarde") { dialog, _ ->
                    dialog.dismiss()
                }
                .show()
        }

        viewModel.mensajeError.observe(this) {
            Toast.makeText(this, it, Toast.LENGTH_LONG).show()
        }
    }

    private fun validarDniLocal(pais: String, dni: String): Boolean {
        val limpio = dni.trim()
        return when (pais.lowercase()) {
            "argentina" -> limpio.length in 7..8
            "brasil"    -> limpio.length == 11
            "chile"     -> limpio.length in 8..9
            "paraguay"  -> limpio.length in 6..7
            else        -> false
        }
    }
}
