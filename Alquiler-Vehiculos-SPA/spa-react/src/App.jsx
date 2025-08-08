import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import Footer from './components/Footer';
import HomeView from './views/HomeView';
import VehicleCatalog from './views/VehicleCatalog';
import LoginView from './views/LoginView';
import RegisterView from './views/RegisterView';
import ContactoView from './views/ContactoView';
import PerfilView from './views/PerfilView';
import OlvidePasswordView from './views/OlvidePasswordView';
import ResetPasswordView from './views/ResetPasswordView'; 
import PromoView from './views/PromoView';

function App() {
  return (
    <Router>
      <NavBar/>
      <main className="mt-5 pt-4">
        <Routes>
          <Route path="/" element={<HomeView />} />
          <Route path="/vehiculos" element={<VehicleCatalog />} />
          <Route path="/login" element={<LoginView />} />
          <Route path="/registro" element={<RegisterView />} />
          <Route path="/contacto" element={<ContactoView />} />
          <Route path="/perfil" element={<PerfilView />} />
          <Route path="/olvide-password" element={<OlvidePasswordView />} />
          <Route path="/restablecer-contrasenia" element={<ResetPasswordView />} />
          <Route path="/promo" element={<PromoView />} />
        </Routes>
      </main>
      <Footer />
    </Router>
  );
}

export default App;