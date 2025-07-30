package com.example.alquilervehiculos.ui.auth.ui.home

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.alquilervehiculos.R
import com.example.alquilervehiculos.adapter.VehiculoAdapter
import com.example.alquilervehiculos.ui.auth.ui.login.LoginActivity
import com.example.alquilervehiculos.ui.auth.ui.alquiler.AlquilerActivity
import com.example.alquilervehiculos.viewmodel.VehiculoViewModel

class HomeActivity : AppCompatActivity() {

    private val homeViewModel: VehiculoViewModel by viewModels()
    private lateinit var recyclerView: RecyclerView
    private lateinit var vehiculoAdapter: VehiculoAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)

        findViewById<TextView>(R.id.tvWelcome).text = "¡Bienvenido a Alquiler Vehículos!"

        val prefs = getSharedPreferences("auth", MODE_PRIVATE)

        findViewById<Button>(R.id.btnLogout).setOnClickListener {
            prefs.edit().remove("token").remove("usuarioId").apply()
            startActivity(Intent(this, LoginActivity::class.java))
            finish()
        }

        recyclerView = findViewById(R.id.rvVehiculos)
        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.setHasFixedSize(true)

        val progressBar = findViewById<ProgressBar>(R.id.progressBar)
        val tvError = findViewById<TextView>(R.id.tvError)

        // ⛔ YA NO creamos alquileres acá. Solo redireccionamos al formulario
        homeViewModel.vehiculos.observe(this) { lista ->
            vehiculoAdapter = VehiculoAdapter(lista) { vehiculo ->
                val intent = Intent(this, AlquilerActivity::class.java)
                intent.putExtra("vehiculoSeleccionado", vehiculo)
                startActivity(intent)
            }
            recyclerView.adapter = vehiculoAdapter
        }

        findViewById<Button>(R.id.btnContacto).setOnClickListener {
            Toast.makeText(this, "¡Pantalla de contacto pronto!", Toast.LENGTH_SHORT).show()
        }

        homeViewModel.isLoading.observe(this) { loading ->
            progressBar.visibility = if (loading) View.VISIBLE else View.GONE
        }

        homeViewModel.error.observe(this) { mensaje ->
            tvError.visibility = if (mensaje != null) View.VISIBLE else View.GONE
            tvError.text = mensaje ?: ""
        }

        homeViewModel.cargarVehiculos()
    }
}