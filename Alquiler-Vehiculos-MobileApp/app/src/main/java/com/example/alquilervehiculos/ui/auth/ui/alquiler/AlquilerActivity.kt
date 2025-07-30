package com.example.alquilervehiculos.ui.auth.ui.alquiler

import android.app.DatePickerDialog
import android.os.Bundle
import android.util.Log
import android.widget.*
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import com.example.alquilervehiculos.R
import com.example.alquilervehiculos.data.model.AlquilerRequest
import com.example.alquilervehiculos.data.model.Vehiculo
import com.example.alquilervehiculos.viewmodel.VehiculoViewModel
import com.google.gson.Gson
import java.text.SimpleDateFormat
import java.util.*
import java.util.concurrent.TimeUnit

class AlquilerActivity : AppCompatActivity() {

    private val viewModel: VehiculoViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_alquiler)

        val vehiculo = intent.getSerializableExtra("vehiculoSeleccionado") as? Vehiculo
        if (vehiculo == null) {
            Toast.makeText(this, "Error: no se encontró el vehículo", Toast.LENGTH_LONG).show()
            finish()
            return
        }

        val tvVehiculoInfo = findViewById<TextView>(R.id.tvVehiculoInfo)
        val etFechaInicio = findViewById<EditText>(R.id.etFechaInicio)
        val etFechaFin = findViewById<EditText>(R.id.etFechaFin)
        val chkTerminos = findViewById<CheckBox>(R.id.chkTerminos)
        val btnConfirmar = findViewById<Button>(R.id.btnConfirmar)
        val spnMetodoPago = findViewById<Spinner>(R.id.spnMetodoPago)
        val tvResultado = findViewById<TextView>(R.id.tvResultado)
        val tvMontoTotal = findViewById<TextView>(R.id.tvMontoTotal)

        tvVehiculoInfo.text = "Vehículo: ${vehiculo.marca} ${vehiculo.modelo} ${vehiculo.anio}"
        val precioPorDia = vehiculo.precioPorDia
        val prefs = getSharedPreferences("auth", MODE_PRIVATE)
        val usuarioId = prefs.getInt("usuarioId", 0)

        fun mostrarDatePicker(campo: EditText) {
            val calendario = Calendar.getInstance()
            val y = calendario.get(Calendar.YEAR)
            val m = calendario.get(Calendar.MONTH)
            val d = calendario.get(Calendar.DAY_OF_MONTH)

            DatePickerDialog(this, { _, year, month, day ->
                val fecha = String.format("%04d-%02d-%02d", year, month + 1, day)
                campo.setText(fecha)

                // 👉 Actualizar total si ambas fechas están completas
                val fi = etFechaInicio.text.toString().trim()
                val ff = etFechaFin.text.toString().trim()
                if (fi.isNotEmpty() && ff.isNotEmpty()) {
                    val dias = calcularDias(fi, ff)
                    val monto = dias * precioPorDia
                    tvMontoTotal.text = "Total estimado: $${monto.toInt()}"
                }
            }, y, m, d).show()
        }

        etFechaInicio.setOnClickListener { mostrarDatePicker(etFechaInicio) }
        etFechaFin.setOnClickListener { mostrarDatePicker(etFechaFin) }

        btnConfirmar.setOnClickListener {
            val fechaInicio = etFechaInicio.text.toString().trim()
            val fechaFin = etFechaFin.text.toString().trim()
            val metodo = spnMetodoPago.selectedItem?.toString() ?: "Efectivo"
            val acepto = chkTerminos.isChecked

            if (fechaInicio.isEmpty() || fechaFin.isEmpty()) {
                Toast.makeText(this, "Completá ambas fechas", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            if (!acepto) {
                Toast.makeText(this, "Debés aceptar los términos", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val usuarioId = prefs.getInt("usuarioId", 0)
            if (usuarioId <= 0) {
                Toast.makeText(this, "Usuario no identificado", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val dias = calcularDias(fechaInicio, fechaFin)
            val monto = dias * precioPorDia
            tvMontoTotal.text = "Total estimado: $${monto.toInt()}"

            val fechaDesdeFormateada = "${fechaInicio.take(10)}T10:00:00"
            val fechaHastaFormateada = "${fechaFin.take(10)}T10:00:00"

            val alquiler = AlquilerRequest(
                usuarioId = usuarioId,
                vehiculoId = vehiculo.vehiculoId,
                empleadoId = 1,
                fechaInicio = fechaDesdeFormateada,
                fechaFin = fechaHastaFormateada,
                estado = "Reservado",
                aceptoTerminos = true
            )

            val gson = Gson()
            Log.d("DEBUG_ALQUILER", gson.toJson(alquiler))

            viewModel.crearAlquiler(alquiler, metodo, monto) { exito, mensaje ->
                tvResultado.text = if (exito) "✔️ $mensaje" else "❌ $mensaje"
                Toast.makeText(this, mensaje, Toast.LENGTH_LONG).show()
                Log.d("ALQUILER_CALLBACK", "Resultado: $mensaje")
                if (exito) finish()
            }
        }
    }

    private fun calcularDias(desde: String, hasta: String): Int {
        val sdf = SimpleDateFormat("yyyy-MM-dd", Locale.getDefault())
        val fecha1 = sdf.parse(desde.substring(0, 10))
        val fecha2 = sdf.parse(hasta.substring(0, 10))
        val diffMillis = fecha2.time - fecha1.time
        return TimeUnit.DAYS.convert(diffMillis, TimeUnit.MILLISECONDS)
            .toInt().coerceAtLeast(1)
    }
}