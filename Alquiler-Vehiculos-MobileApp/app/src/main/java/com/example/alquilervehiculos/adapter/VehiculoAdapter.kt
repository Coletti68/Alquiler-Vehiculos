package com.example.alquilervehiculos.adapter
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.alquilervehiculos.R
import com.example.alquilervehiculos.data.model.Vehiculo

class VehiculoAdapter(
    private val vehiculos: List<Vehiculo>,
    private val onAlquilarClick: (Vehiculo) -> Unit
) : RecyclerView.Adapter<VehiculoAdapter.VehiculoViewHolder>() {

    private var selectedPosition = -1

    inner class VehiculoViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val tvMarca: TextView = itemView.findViewById(R.id.tvMarca)
        val tvModelo: TextView = itemView.findViewById(R.id.tvModelo)
        val tvPlaca: TextView = itemView.findViewById(R.id.tvPlaca)
        val btnAlquilar: Button = itemView.findViewById(R.id.btnEdit)
        val contenedor: View = itemView // para cambiarle el fondo al seleccionar
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): VehiculoViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.vehiculo_list_item, parent, false)
        return VehiculoViewHolder(view)
    }

    override fun onBindViewHolder(holder: VehiculoViewHolder, position: Int) {
        val vehiculo = vehiculos[position]
        holder.tvMarca.text = vehiculo.marca
        holder.tvModelo.text = vehiculo.modelo
        holder.tvPlaca.text = vehiculo.placa

        // 🖼 Mostrar imagen según el color del vehículo
        val imgView = holder.itemView.findViewById<ImageView>(R.id.ivVehiculo)
        val contexto = holder.itemView.context

        val imagenes = listOf(
            R.drawable.rojo,
            R.drawable.gris,
            R.drawable.plata,
            R.drawable.negro,
            R.drawable.plata
        )

        val index = position % imagenes.size
        imgView.setImageResource(imagenes[index])

        // 🔄 Color de fondo si está seleccionado
        holder.contenedor.setBackgroundResource(
            if (position == selectedPosition) R.drawable.bg_item_selected
            else android.R.color.transparent
        )

        // 👉 Selección visual del ítem
        holder.contenedor.setOnClickListener {
            selectedPosition = position
            notifyDataSetChanged()
        }

        // ✅ Botón de selección
        holder.btnAlquilar.setOnClickListener {
            onAlquilarClick(vehiculo)
        }
    }

    override fun getItemCount(): Int = vehiculos.size
}