import BienvenidaHero from '../components/BienvenidaHero';
import InfoContacto from '../components/InfoContacto';
import PromoQRWidget from '../components/PromoQRWidget';

export default function HomeView() {
  return (
    <section id="home-section">
       <div>
      <BienvenidaHero />
      <InfoContacto />
      <PromoQRWidget />
       </div>
      
    </section>
  );
}