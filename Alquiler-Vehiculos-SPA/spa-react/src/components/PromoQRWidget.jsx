import { useState } from 'react';

export default function PromoQRWidget() {
  const [visible, setVisible] = useState(false);
  const urlPromo = 'https://rentcarsgoya.com/promo'; // Cambiar si estás en localhost

  return (
    <>
      {/* Botón flotante para mostrar/ocultar */}
      <button
        onClick={() => setVisible(!visible)}
        style={{
          position: 'fixed',
          bottom: '20px',
          right: '20px',
          zIndex: 999,
          backgroundColor: '#3498db',
          color: 'white',
          border: 'none',
          borderRadius: '50%',
          width: '50px',
          height: '50px',
          fontSize: '24px',
          cursor: 'pointer',
          boxShadow: '0 4px 10px rgba(0,0,0,0.2)',
        }}
        title={visible ? 'Cerrar Promo' : 'Ver Promo'}
      >
        🎁
      </button>

      {/* Contenedor QR flotante */}
      {visible && (
        <div style={{
          position: 'fixed',
          bottom: '80px',
          right: '20px',
          width: '220px',
          padding: '15px',
          backgroundColor: 'white',
          borderRadius: '12px',
          boxShadow: '0 4px 10px rgba(0,0,0,0.15)',
          textAlign: 'center',
          fontFamily: 'Arial, sans-serif',
          zIndex: 998,
          transition: 'opacity 0.3s ease-in-out',
        }}>
          <h5 style={{ color: '#3498db', marginBottom: '10px' }}>🎁 Promo ahora</h5>
         <img
            src={`https://api.qrserver.com/v1/create-qr-code/?size=180x180&data=${encodeURIComponent(urlPromo)}`}
            alt="QR Promoción dinámica"
            style={{ borderRadius: '10px', border: '2px solid #3498db' }}
          />
          <p style={{ fontSize: '12px', marginTop: '8px' }}>Escaneá para ver tu descuento</p>
        </div>
      )}
    </>
  );
}