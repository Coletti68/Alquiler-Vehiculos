import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './assets/style.css';

//IMPORTA ESTILOS Y APP
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)