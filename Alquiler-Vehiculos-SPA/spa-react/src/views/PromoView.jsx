export default function PromoView() {
  const promos = [10, 20, 30];
  const intervalo = 10 * 60 * 1000; // 10 minutos
  const ahora = Date.now();
  const indice = Math.floor(ahora / intervalo) % promos.length;
  const promoActual = promos[indice];

  return (
    <div style={{
      padding: '2rem',
      textAlign: 'center',
      fontFamily: 'Arial, sans-serif',
      color: '#2c3e50'
    }}>
      <h2>🎁 Promo del momento</h2>
      <p>¡Tenés un <strong>{promoActual}%</strong> de descuento en RentCars Goya!</p>
      <small>(Esta promo cambia cada 10 minutos)</small>
    </div>
  );
}