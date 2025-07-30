package com.example.alquilervehiculos.network

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import okhttp3.OkHttpClient
import javax.net.ssl.*
import java.security.SecureRandom
import java.security.cert.X509Certificate

object RestClient {
    private const val BASE_URL = "https://10.0.2.2:5001/api/"

    // 👉 TrustManager que acepta todos los certificados (solo para desarrollo)
    private val trustAllCerts = arrayOf<TrustManager>(
        object : X509TrustManager {
            override fun checkClientTrusted(chain: Array<X509Certificate>, authType: String) {}
            override fun checkServerTrusted(chain: Array<X509Certificate>, authType: String) {}
            override fun getAcceptedIssuers(): Array<X509Certificate> = arrayOf()
        }
    )

    private val sslContext: SSLContext = SSLContext.getInstance("SSL").apply {
        init(null, trustAllCerts, SecureRandom())
    }

    private val okHttpClient: OkHttpClient = OkHttpClient.Builder()
        .connectTimeout(15, java.util.concurrent.TimeUnit.SECONDS) // ⏱ conecta al server
        .readTimeout(15, java.util.concurrent.TimeUnit.SECONDS)    // ⏱ espera la respuesta
        .writeTimeout(15, java.util.concurrent.TimeUnit.SECONDS)   // ⏱ opcional, por si subís archivos
        .sslSocketFactory(sslContext.socketFactory, trustAllCerts[0] as X509TrustManager)
        .hostnameVerifier { _, _ -> true }
        .build()

    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(okHttpClient)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }

    val api: ApiService by lazy {
        retrofit.create(ApiService::class.java)
    }
}